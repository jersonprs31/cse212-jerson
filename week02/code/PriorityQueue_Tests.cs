using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 - Test every requirement of the priority queue.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue Low (1), Medium (3), and High (8), in that order.
    // Expected Result: Enqueue keeps FIFO insertion order. Dequeue returns High,
    // then Medium, then Low, including the item originally at the very back.
    // Defect(s) Found: Dequeue did not examine the last queue item and did not remove a returned item.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 8);

        Assert.AreEqual("[Low (Pri:1), Medium (Pri:3), High (Pri:8)]", priorityQueue.ToString());
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with the same highest priority, plus lower-priority items.
    // Expected Result: First high-priority item is removed before Second; lower-priority
    // items follow in descending priority order.
    // Defect(s) Found: Using >= instead of > selected the later item in a priority tie.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Middle", 3);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Middle", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from a new empty priority queue.
    // Expected Result: InvalidOperationException with the exact message "The queue is empty."
    // Defect(s) Found: None identified; the original empty-queue check was correct.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(() =>
        {
            priorityQueue.Dequeue();
        });
        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Enqueue one item, dequeue it, and attempt to dequeue again.
    // Expected Result: The first call returns "Only"; the second throws an
    // InvalidOperationException with the exact empty-queue message.
    // Defect(s) Found: Dequeue originally returned an item without removing it.
    public void TestPriorityQueue_RemovesItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only", 10);

        Assert.AreEqual("Only", priorityQueue.Dequeue());

        var exception = Assert.ThrowsException<InvalidOperationException>(() =>
        {
            priorityQueue.Dequeue();
        });
        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Use negative, zero, and positive priorities.
    // Expected Result: Items come out in numeric descending priority order:
    // Positive, Zero, LessNegative, MoreNegative.
    // Defect(s) Found: Skipping the last item and not removing values caused incorrect results.
    public void TestPriorityQueue_NegativeAndZeroPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("MoreNegative", -10);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("LessNegative", -2);
        priorityQueue.Enqueue("Positive", 7);

        Assert.AreEqual("Positive", priorityQueue.Dequeue());
        Assert.AreEqual("Zero", priorityQueue.Dequeue());
        Assert.AreEqual("LessNegative", priorityQueue.Dequeue());
        Assert.AreEqual("MoreNegative", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue a high-priority item after dequeuing another item.
    // Expected Result: The newly enqueued high-priority item comes out first,
    // then the older low-priority item.
    // Defect(s) Found: Dequeue originally failed to remove items, so subsequent
    // operations used stale entries.
    public void TestPriorityQueue_EnqueueAfterDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("OldLow", 1);
        priorityQueue.Enqueue("OldHigh", 4);
        Assert.AreEqual("OldHigh", priorityQueue.Dequeue());

        priorityQueue.Enqueue("NewHigh", 9);
        Assert.AreEqual("NewHigh", priorityQueue.Dequeue());
        Assert.AreEqual("OldLow", priorityQueue.Dequeue());
    }
}
