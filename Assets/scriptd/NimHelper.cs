using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimHelper {
    public static int CalculateMatchestoRemove(int remainingMatches){
        int MatchestoRemove = (remainingMatches - 1) % 5;

        if( MatchestoRemove == 0){
            return 1;
        } else {
            return MatchestoRemove;
        }
    }
}