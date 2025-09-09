// <copyright file="LaneOrderTest.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Utils.Test;

[TestFixture]
[TestOf(typeof(LaneOrder))]
public class LaneOrderTest
{
    [Test]
    [TestCase(1, 1, new[] { 1 })]
    [TestCase(1, 2, new[] { 1, 2 })]
    [TestCase(1, 3, new[] { 2, 1, 3 })]
    [TestCase(1, 4, new[] { 2, 3, 1, 4 })]
    [TestCase(1, 5, new[] { 3, 2, 4, 1, 5 })]
    [TestCase(1, 6, new[] { 3, 4, 2, 5, 1, 6 })]
    [TestCase(1, 7, new[] { 4, 3, 5, 2, 6, 1, 7 })]
    [TestCase(1, 8, new[] { 4, 5, 3, 6, 2, 7, 1, 8 })]
    [TestCase(2, 7, new[] { 4, 5, 3, 6, 2, 7 })]
    public void TestGetLaneOrder(int startLane, int endLane, int[] expectedLaneOrder)
    {
        IEnumerable<int> laneOrder = LaneOrder.GetLaneOrder(startLane, endLane);
        Assert.That(laneOrder, Is.EquivalentTo(expectedLaneOrder));
    }
}
