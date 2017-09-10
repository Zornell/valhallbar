using System;

public class EnemyKilledEventArgs : EventArgs
{
    public int Lane { get; set; }
    public int EnemyType { get; set; }
}