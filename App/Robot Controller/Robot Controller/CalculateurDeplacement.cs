using System;
using System.Collections.Generic;
using System.Text;

namespace Robot_Controller
{
    internal class CalculateurDeplacement
    {

        public List<Commande> ListeCommande { get; set; }
        public int Vitesse { get; set; }
        private Leap.Vector relativePosition;
        private static int marge = 60;
        private static int margeY = 50;

        public CalculateurDeplacement()
        {
            ListeCommande = new List<Commande>();
            relativePosition = new Leap.Vector();
        }


        public void Reset()
        {
            ListeCommande.Clear();
            Vitesse = 0;
        }


        public void CalculDeplacementQuatreAxe(HandLeap startPosition, HandLeap actualPosition)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;

            if (relativePosition.x < -marge && !ListeCommande.Contains(Commande.GAUCHE))
            {
                ListeCommande.Add(Commande.GAUCHE);
            }
            
            if (relativePosition.x > marge && !ListeCommande.Contains(Commande.DROITE))
            {
                ListeCommande.Add(Commande.DROITE);
            }

            if (relativePosition.z < -marge && !ListeCommande.Contains(Commande.HAUT))
            {
                ListeCommande.Add(Commande.HAUT);
            }
            
            if (relativePosition.z > marge && !ListeCommande.Contains(Commande.BAS))
            {
                ListeCommande.Add(Commande.BAS);
            }

            if (relativePosition.x >= -marge && relativePosition.x <= marge && (ListeCommande.Contains(Commande.GAUCHE) || ListeCommande.Contains(Commande.DROITE)))
            {
                ListeCommande.Remove(Commande.GAUCHE);
                ListeCommande.Remove(Commande.DROITE);
            }

            if (relativePosition.z >= -marge && relativePosition.z <= marge && (ListeCommande.Contains(Commande.HAUT) || ListeCommande.Contains(Commande.BAS)))
            {
                ListeCommande.Remove(Commande.HAUT);
                ListeCommande.Remove(Commande.BAS);
            }
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


        public void CalculVitesse(HandLeap startPosition, HandLeap actualPosition)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;

            if(relativePosition.y > margeY)
            {
                Vitesse = (int)relativePosition.y / 2 + 50 - margeY/2;
            }

            if(relativePosition.y < -margeY)
            {
                Vitesse = (int)relativePosition.y / 2 + 50 + margeY/2;
            }

            if(relativePosition.y <= margeY && relativePosition.y >= -margeY)
            {
                Vitesse = 50;
            }


            if (Vitesse < 10) Vitesse = 10;
            if (Vitesse > 100) Vitesse = 100;
        }
    }
}
