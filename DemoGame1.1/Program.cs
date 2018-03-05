using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using DemoGame1._1;

namespace DemoGamev1._1
{
    class Program
    {

        static void Main(string[] args)
        {
        
            //start music
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Tribal Ritual.wav";
            player.PlayLooping();
            Console.WriteLine(" ____  __.__                   .___                       _____    _________                   .__        ");
            Console.WriteLine("|    |/ _|__| ____    ____   __| _/____   _____     _____/ ____\\  /   _____/____  _____ _______|__|____   ");
            Console.WriteLine("|      < |  |/    \\  / ___\\ / __ |/  _ \\ /     \\   /  _ \\   __\\   \\_____  \\__  \\ \\__  \\_  __ \\  \\__  \\  ");
            Console.WriteLine("|    |  \\|  |   |  \\/ /_/  > /_/ (  <_> )  Y Y  \\ (  <_> )  |     /        \\/ __ \\_/ __ \\|  | \\/  |/ __ \\_");
            Console.WriteLine("|____|__ \\__|___|  /\\___  /\\____ |\\____/|__|_|  /  \\____/|__|    /_______  (____  (____  /__|  |__(____  /");
            Console.WriteLine("        \\/       \\//_____/      \\/            \\/                         \\/     \\/     \\/              \\/ ");
            Console.WriteLine("     ____.            .__/\\         __________      ___.          .__  .__  .__                           ");
            Console.WriteLine("    |    |____ _______|  )/  ______ \\______   \\ ____\\_ |__   ____ |  | |  | |__| ____   ____              ");
            Console.WriteLine("    |    \\__  \\_  __ \\  |  /  ___/  |       _// __ \\| __ \\_/ __ \\|  | |  | |  |/  _ \\ /    \\             ");
            Console.WriteLine("/\\__|    |/ __ \\|  | \\/  |__\\___ \\   |    |   \\  ___/| \\_\\ \\  ___/|  |_|  |_|  (  <_> )   |  \\            ");
            Console.WriteLine("\\________(____  /__|  |____/____  >  |____|_  /\\___  >___  /\\___  >____/____/__|\\____/|___|  /            ");
            Console.WriteLine("              \\/                \\/          \\/     \\/    \\/     \\/                         \\/       ");
            Console.ReadLine();
            Start:
            //initialize player stat variables

            //A method on the Player class should initializes the variables

            Random rnd = new Random();
            int playerBaseAC = 10;
            int playerArmorAC = 0;
            int playerAB = 10;
            int postCombatHealing = 2;
            int playerHP = 10;
            int playerBaseDamage = 2;
            Console.WriteLine("What is your name, adventurer?");
            string playerName = Console.ReadLine();
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Billy Bob Blank";
            }

            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Billy Bob Blank";
            }
            int prevStage = 0;
            //randomization code

            //intro
            Console.WriteLine("\nYou, " + playerName + ", are a mercenary warrior from the Kingdom of Saaria.");
            Console.WriteLine("The king Jan II has hired you to assasinate a rogue Jarl.");
            Console.WriteLine("In order to get to the Jarl, you must first dispatch of his assortment of bodyguards.");
            Console.WriteLine("You start with the first bodyguard, who you challenge to combat.\n");
            Console.ReadLine();



        //precombat
        Precombat:

            int stage = prevStage + 1;
            prevStage = stage;
           
            int villainBaseAC = 10;
            int villainArmorAC = 0;
            int villainAB = stage;
         
            int villainBD = 1;

            //Villain system for combat, (villainId = current stage)
            AllVillains villain = new AllVillains();
            string crntVillain = "Test Monster";
            int crntVillainHp = 1;
           
            

            foreach (var v in from vill in villain
                              where vill.villainId == stage
                              select vill)
            {
                crntVillain = v.villainName;
                crntVillainHp = v.villainHp;
                villainAB = v.villainAttackBonus;
                villainBD = v.villainBaseDamage;
            }

            //enter combat text

            Console.WriteLine("You have encountered a wild " + crntVillain);
            Console.ReadLine();

            //WpnRandomization
            //To use weapon call the variables wpnUsed & wpnAB
            //remember to include also the wpn randomizer to the loop 
            //so it won't use just one weapon with all the villains

            AllWeapons weapon = new AllWeapons();
            string wpnUsed = "None";
            int wpnAB = 2;

            
            int rndWpn = rnd.Next(0, 15);
            int wpnMinBD = 0;
            int wpnMaxBD = 0;

            foreach (var w in from wpn in weapon
                              where wpn.weaponId == rndWpn
                              select wpn)
            {
                wpnUsed = w.weaponName;
                wpnAB = w.damageBonus;
                wpnMinBD = w.wpnMinBaseDamage;
                wpnMaxBD = w.wpnMaxBaseDamage;
            }

            //Console.WriteLine("WeaponBD: " + rnd.Next(wpnMinBD, wpnMaxBD)); 

            //combat

            while (playerHP > 0)
            {
                int playerAttackRoll = rnd.Next(1, 21);
                int villainAttackRoll = rnd.Next(1, 21);
                if ((playerAttackRoll + playerAB) < (villainBaseAC + villainArmorAC))
                {
                    Console.WriteLine("You roll " + playerAttackRoll + " + " + playerAB + " against " + crntVillain + "'s armorclass of " + (villainBaseAC + villainArmorAC) + ". You miss.");
                }
                else
                {
                    Console.WriteLine("You roll " + playerAttackRoll + " + " + playerAB + " against " + crntVillain + "'s armorclass of " + (villainBaseAC + villainArmorAC) + ". You hit " + crntVillain + " for " + playerBaseDamage + " damage.");
                    crntVillainHp = crntVillainHp - playerBaseDamage;
                    Console.WriteLine(crntVillain + " has " + crntVillainHp + " HP left.");
                }
                Console.ReadLine();
                if (crntVillainHp < 1)
                {
                    break;
                }
                if ((villainAttackRoll + villainAB) < (playerBaseAC + playerArmorAC))
                {
                    Console.WriteLine(crntVillain + " rolls " + villainAttackRoll + " + " + villainAB + " against " + playerName + "'s armorclass of " + (playerBaseAC + playerArmorAC) + ". " + crntVillain + " misses.");
                }
                else
                {
                    Console.WriteLine(crntVillain + " rolls " + villainAttackRoll + " + " + villainAB + " against " + playerName + "'s armorclass of " + (playerBaseAC + playerArmorAC) + ". " + crntVillain + " attacks you for " + villainBD + " damage.");
                    playerHP = playerHP - villainBD;
                    Console.WriteLine(playerName + " has " + playerHP + " HP left.");
                    Console.ReadLine();
                }

            }

            //death, if it happens
            if (playerHP < 1)
            {
                Console.WriteLine("\nYou have died. The Jarl no longer won't have his plans ruined.\n");
                Console.ReadKey();
                Console.WriteLine("Willing to play more?(Y/N)");
                string key = Console.ReadLine();
                if (key.StartsWith("y") | key.StartsWith("Y"))
                {
                    goto Start;
                }
                else
                {
                    goto End;
                }
            };
            //end of combat story


            if (stage == 1)
            {
                Console.WriteLine("You have defeated the first bodyguard.");
                Console.WriteLine("You decide to have a lunch before the next bodyguard.");
                Console.WriteLine("You eat some swede soup. The second bodyguard interrupts your lunch.");
                Console.WriteLine("What a douchebag.");
            };

            if (stage == 2)
            {
                Console.WriteLine("You have defeated the second bodyguard.");
                Console.WriteLine("The swede soup has spilled all over the floor.");
                Console.WriteLine("You decide to eat salmon instead.");
                Console.WriteLine("However, your lunch was once again interrupted by a bodyguard.");
            };
            if (stage == 3)
            {
                Console.WriteLine("You have defeated the third bodyguard.");
                Console.WriteLine("The salmon has fallen to the ground, and is all dirty.");
                Console.WriteLine("By this point you've already lost your appetite.");
                Console.WriteLine("You decide to move on towards the Jarl's domain.");
                Console.WriteLine("While on the road, a fourth bodyguard attacks you.");
            };
            if (stage == 4)
            {
                Console.WriteLine("You have defeated the fourth bodyguard.");
                Console.WriteLine("When continuing the trail, you notice a letter given to the bodyguard.");
                Console.WriteLine("Unfortunetaly, you're illiterate, so you ignore the letter.");
                Console.WriteLine("You get attacked by another bodyguard.");
            };
            if (stage == 5)
            {
                Console.WriteLine("You have defeated the fifth bodyguard.");
                Console.WriteLine("Before dying, the bodyguard mumbles something about the Jarl and names.");
                Console.WriteLine("You are not so interested. So you move on.");
                Console.WriteLine("You arrive at Jarl's personal domain.");
                Console.WriteLine("You are greeted by an attacking bodyguard. How nice!");
            };
            if (stage == 6)
            {
                Console.WriteLine("You have defeated the sixth bodyguard.");
                Console.WriteLine("You see some scared villagers hiding from you.");
                Console.WriteLine("You tell them you're here to bring peace.");
                Console.WriteLine("While trying to explain your intentions, a bodyguard attacks you from behind.");
            };
            if (stage == 7)
            {
                Console.WriteLine("You have defeated the seventh bodyguard.");
                Console.WriteLine("You ponder on several things.");
                Console.WriteLine("Why does the Jarl hire bodyguards from very different backgrounds?");
                Console.WriteLine("Why has the Jarl betrayed the king?");
                Console.WriteLine("Where can I get some food around here?");
                Console.WriteLine("Then suddenly, you notice another bodyguard attacking you.");
            };
            if (stage == 8)
            {
                Console.WriteLine("You have defeated the eighth bodyguard.");
                Console.WriteLine("You approach the Jarl's palace.");
                Console.WriteLine("A bodyguard is stationed near the entrance, guarding it.");
                Console.WriteLine("You decide to strike them before they strike you.");
            };
            if (stage == 9)
            {
                Console.WriteLine("You have defeated the final bodyguard.");
                Console.WriteLine("You break the Jarl's door, and shout obscenities at him.");
                Console.WriteLine("The Jarl seems confused, but defends himself.");
                Console.WriteLine("You begin the final battle.");
            };
            playerHP = playerHP + 2;
            Console.WriteLine("You gain " + postCombatHealing + " HP.\n");

            //nextstage
            if (stage < 11)
            {
                goto Precombat;
            }





            //end texts
            Console.ReadLine();
            if (String.Equals(playerName, "Timo"))
            {
                Console.WriteLine("After you have defeated the Jarl, you grab his necklace to bring to the king.");
                Console.WriteLine("After you present the necklace to the king, he shouts from his throne.");
                Console.WriteLine("'CONGRATULATIONS! YOU HAVE EARNED A COFFEE BREAK!'");
                Console.WriteLine("For this reason, the king lets you drink coffee publicly.");
            }
            else
            {
                Console.WriteLine("After you have defeated the Jarl, you grab his necklace to bring to the king.");
                Console.WriteLine("After you present the necklace to the king, he shouts from his throne.");
                Console.WriteLine("'THAT WAS THE WRONG JARL. THIS ONE WAS MY MOST LOYAL, YOU MONGREL!'");
                Console.WriteLine("For this treason, the king has you executed publicly.");
            }
            End:;

            //https://github.com/otuju004/
            //https://github.com/JustusJuutilainen/FS_Justuus
        }
    }
}

