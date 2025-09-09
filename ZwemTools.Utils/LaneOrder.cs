// <copyright file="LaneOrder.cs" company="ZwemTools">
// Copyright (c) ZwemTools. All rights reserved.
// </copyright>

namespace ZwemTools.Utils;

/// <summary>
/// Lane order utilities.
/// </summary>
public static class LaneOrder
{
    /// <summary>
    /// Returns the order of the lanes use for assigning athletes to heats.
    /// </summary>
    /// <param name="startLane">The leftmost lane.</param>
    /// <param name="endLane">The rightmost lane.</param>
    /// <returns>The order of the lanes.</returns>
    public static IEnumerable<int> GetLaneOrder(int startLane, int endLane)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(endLane, startLane);

        int count = endLane - startLane + 1;
        int firstLane = (int)Math.Floor((startLane + endLane) / 2d);
        int currentLane = firstLane;

        int nextSign = count % 2 == 0 ? 1 : -1;

        for (int i = 1; i <= count; i++)
        {
            yield return currentLane;

            currentLane += nextSign * i;
            nextSign *= -1;
        }
    }
}
