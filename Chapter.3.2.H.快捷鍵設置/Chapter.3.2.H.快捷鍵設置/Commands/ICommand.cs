﻿namespace Chapter._3._2.H.快捷鍵設置.Commands;

public interface ICommand
{
    public string Name { get; }
    public void Execute();
    public void Undo();
}