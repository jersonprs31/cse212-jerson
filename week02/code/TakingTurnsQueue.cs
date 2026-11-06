using System;

/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to
/// the back of the queue (per FIFO rules). GetNextPerson removes the next person,
/// then re-enqueues them if they have turns remaining. A turns value of 0 or
/// less means the person has an infinite number of turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();
    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the back of the queue.
    /// </summary>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Return the next person. Re-enqueue a person with remaining finite turns,
    /// or a person whose initial turns value indicates infinite turns.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // 0 and negative turn counts mean infinite turns: never decrement.
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            person.Turns -= 1;
            _people.Enqueue(person);
        }

        // One remaining finite turn means this is the person's last turn.
        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
