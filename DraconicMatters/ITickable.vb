Public Interface ITickable
    Property TickableWhenPaused As Boolean
    Sub Tick(deltaTimeMS As Double)
End Interface
