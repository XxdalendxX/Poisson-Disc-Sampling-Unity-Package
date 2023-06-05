using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace TurnBasedSystem
{
    /// <summary>
    /// This would carry nessesary information for the management of the network system.
    /// </summary>
    public class TBManager : NetworkBehaviour
    {
        List<TBPlayer> TB_players;
        int TB_turnNo = 0;

        [Server]
        public virtual void TB_StartGame()
        {
            if (TB_turnNo != 0)
                TB_turnNo = 0;
            if (TB_players == null)
                return;
            TB_players[TB_turnNo].RpcTB_ToggleTurn();
        }

        [Server]
        public virtual void TB_EndTurn(TBPlayer player)
        {
            TB_players[TB_turnNo].RpcTB_ToggleTurn();
        }

        [Server]
        public virtual void TB_NextTurn(TBPlayer player)
        {
            TB_turnNo++;
            if (TB_turnNo == TB_players.Count)
            {
                TB_turnNo = 0;
            }
            TB_players[TB_turnNo].RpcTB_ToggleTurn();
        }

        [Server]
        public virtual void TB_GameEnd(TBPlayer player)
        {
            player.RpcTB_PlayerWon();
        }

        public int TB_GetPlayerTurn()
        {
            if (TB_players == null)
                return 0;
            else
                return TB_turnNo + 1;
        }


    }
}
