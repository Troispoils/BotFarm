using Client;
using Client.AI;
using Client.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFarm.AI
{
    class FollowGroupLeaderAI : IStrategicAI
    {
        int scheduledAction;
        AutomatedGame game;

        public bool Activate(AutomatedGame game)
        {
            this.game = game;
            ScheduleFollowLeader();
            return true;
        }

        void ScheduleFollowLeader()
        {
            scheduledAction = game.ScheduleAction(() =>
            {
                if (!game.Player.IsAlive)
                    return;

                // Check if we are in a party and follow the party leader
                if (game.GroupLeaderGuid == 0)
                    return;

                WorldObject groupLeader;
                if (game.Objects.TryGetValue(game.GroupLeaderGuid, out groupLeader))
                {
                    game.CancelActionsByFlag(ActionFlag.Movement);
                    game.Follow(groupLeader);

                    //Console.WriteLine("Vie du leader : " + game.vieLeader);
                    if (game.vieLeader < 50 && game.vieLeader > 0)
                    {
                        game.CancelActionsByFlag(ActionFlag.Movement);
                        game.TargetLeader();
                        game.CastSpellid(0);
                    }
                }

                Console.WriteLine(groupLeader.GetPosition().ToString());
                Console.SetCursorPosition(0, Console.CursorTop - 1);

            }, DateTime.Now.AddSeconds(1), new TimeSpan(0, 0, 1));
        }

        public bool AllowPause()
        {
            return true;
        }

        public void Deactivate()
        {
            game.CancelAction(scheduledAction);
        }

        public void Pause()
        {
            game.CancelAction(scheduledAction);
        }

        public void Resume()
        {
            ScheduleFollowLeader();
        }

        public void Update()
        {
        }
    }
}
