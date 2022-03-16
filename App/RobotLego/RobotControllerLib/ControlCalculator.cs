using System;
using System.Collections.Generic;
using System.Text;

namespace RobotControllerLib
{
    /// <summary>
    /// class containing all the calculations to detect a hand movement
    /// </summary>
    public class ControlCalculator
    {
        /// <summary>
        /// list of current commands
        /// </summary>
        public List<Commande> ListeCommande { get; set; }
        /// <summary>
        /// speed indicator 
        /// </summary>
        public int Speed { get; set; } = 50;
        /// <summary>
        /// use for absolute position towards the starting position
        /// </summary>
        private Leap.Vector relativePosition;
        private static readonly int margeX = 60; //margin for forward and backward movement
        private static readonly int margeZ = 40; //margin for sideways movement
        private static readonly int margeY = 30; //high-low margin for speed

        public ControlCalculator()
        {
            ListeCommande = new List<Commande>();
            relativePosition = new Leap.Vector();
        }

        /// <summary>
        /// reset the list of current commands and the speed
        /// </summary>
        public void ResetAll()
        {
            ListeCommande.Clear();
            Speed = 50;
        }

        /// <summary>
        /// reset the speed
        /// </summary>
        public void ResetSpeed()
        {
            Speed = 50;
        }

        /// <summary>
        /// calculates a position relative to the starting position and compares it to the margin to see if a direction is detached
        /// </summary>
        /// <param name="startPosition">the starting position right hand</param>
        /// <param name="currentPosition">the current position of the right hand</param>
        /// <param name="autoSpeed">true to automatically calculate the motors speed / false for always have the same motors speed </param>
        public void FourAxisMouvCalculation(HandLeap startPosition, HandLeap currentPosition, bool autoSpeed = true)
        {
            relativePosition = currentPosition.PalmPosition - startPosition.PalmPosition;

            if (relativePosition.x < -margeX && !ListeCommande.Contains(Commande.GAUCHE))
            {
                ListeCommande.Add(Commande.GAUCHE);
            }

            if (relativePosition.x > margeX && !ListeCommande.Contains(Commande.DROITE))
            {
                ListeCommande.Add(Commande.DROITE);
            }

            if (relativePosition.z < -margeZ && !ListeCommande.Contains(Commande.HAUT))
            {
                ListeCommande.Add(Commande.HAUT);
            }

            if (relativePosition.z > margeZ && !ListeCommande.Contains(Commande.BAS))
            {
                ListeCommande.Add(Commande.BAS);
            }

            if (relativePosition.x >= -margeX && relativePosition.x <= margeX && (ListeCommande.Contains(Commande.GAUCHE) || ListeCommande.Contains(Commande.DROITE)))
            {
                ListeCommande.Remove(Commande.GAUCHE);
                ListeCommande.Remove(Commande.DROITE);
            }

            if (relativePosition.z >= -margeZ && relativePosition.z <= margeZ && (ListeCommande.Contains(Commande.HAUT) || ListeCommande.Contains(Commande.BAS)))
            {
                ListeCommande.Remove(Commande.HAUT);
                ListeCommande.Remove(Commande.BAS);
            }

            if (autoSpeed) SetAutoSpeed();
        }

        /// <summary>
        /// observes the twist of the wrist
        /// </summary>
        /// <param name="Position">current position of the right hand (contains a torsion indicator)</param>
        public void CalculRotation(HandLeap Position)
        {
            if (Position.Rotation.z > 0.3 && !ListeCommande.Contains(Commande.TOURNERGAUCHE))
            {
                ListeCommande.Add(Commande.TOURNERGAUCHE);
            }

            if (Position.Rotation.z < -0.4 && !ListeCommande.Contains(Commande.TOURNERDROITE))
            {
                ListeCommande.Add(Commande.TOURNERDROITE);
            }

            if (Position.Rotation.z <= 0.3 && Position.Rotation.z >= -0.4 && (ListeCommande.Contains(Commande.TOURNERGAUCHE) || ListeCommande.Contains(Commande.TOURNERDROITE)))
            {
                ListeCommande.Remove(Commande.TOURNERGAUCHE);
                ListeCommande.Remove(Commande.TOURNERDROITE);
            }
        }

        /// <summary>
        /// calculates a position relative to the starting position and compares it to the margin to see if the hand is higher or lower (increase / decrease speed)
        /// </summary>
        /// <param name="startPosition">the starting position left hand</param>
        /// <param name="currentPosition">the current position left hand</param>
        public void CalculSpeed(HandLeap startPosition, HandLeap currentPosition)
        {
            relativePosition = currentPosition.PalmPosition - startPosition.PalmPosition;

            if (relativePosition.y > margeY)
            {
                Speed = (int)relativePosition.y / 2 + 50 - margeY / 2;
            }

            if (relativePosition.y < -margeY)
            {
                Speed = (int)relativePosition.y / 2 + 50 + margeY / 2;
            }

            if (relativePosition.y <= margeY && relativePosition.y >= -margeY)
            {
                Speed = 50;
            }

            if (Speed < 10) Speed = 10;
            if (Speed > 100) Speed = 100;
        }

        /// <summary>
        /// calculates the speed so that the robot moves at the same speed going straight, diagonally and sideways
        /// </summary>
        public void SetAutoSpeed()
        {
            if(ListeCommande.Count == 1)
            {
                if (ListeCommande.Contains(Commande.HAUT) || ListeCommande.Contains(Commande.BAS)) Speed = 50;
                if (ListeCommande.Contains(Commande.GAUCHE) || ListeCommande.Contains(Commande.DROITE)) Speed = 60;
            }
            if(ListeCommande.Count == 2)
            {
                Speed = 70;
            }
        }

        /// <summary>
        /// to display the commands list
        /// </summary>
        /// <returns></returns>
        public String ToStringCommande()
        {
            String s = "";
            foreach(Commande c in ListeCommande)
            {
                s += c.ToString();
            }

            return s;
        }
    }
}
