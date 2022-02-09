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

        public CalculateurDeplacement()
        {
            ListeCommande = new List<Commande>();
            relativePosition = new Leap.Vector();
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

            Vitesse = (int)relativePosition.y/2 + 50;

            if (Vitesse < 20) Vitesse = 20;
            if (Vitesse > 100) Vitesse = 100;
        }
    }
}
