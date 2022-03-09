using System;
using System.Collections.Generic;
using System.Text;

namespace RobotControllerLib
{
    public class ControlCalculator
    {
        public List<Commande> ListeCommande { get; set; }
        public int Speed { get; set; } = 50;
        private Leap.Vector relativePosition;
        private static readonly int margeX = 60; //marge pour les déplacements avant / arrière
        private static readonly int margeZ = 40; //marge pour les déplacements de côté
        private static readonly int margeY = 30; //marge haut bas pour la vitesse

        public ControlCalculator()
        {
            ListeCommande = new List<Commande>();
            relativePosition = new Leap.Vector();
        }

        public void ResetAll()
        {
            ListeCommande.Clear();
            Speed = 50;
        }

        public void ResetSpeed()
        {
            Speed = 50;
        }

        public void CalculDeplacementQuatreAxe(HandLeap startPosition, HandLeap actualPosition, bool autoSpeed = true)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;

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

        public void CalculSpeed(HandLeap startPosition, HandLeap actualPosition)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;

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
