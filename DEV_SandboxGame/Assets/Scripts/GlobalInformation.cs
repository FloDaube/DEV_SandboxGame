using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MultiplayerMode
{
    Host,
    Client
}

public static class GlobalInformation
{
    public static MultiplayerMode multiplayerMode;
}
