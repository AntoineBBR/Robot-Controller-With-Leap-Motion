using System;
using System.Collections.Generic;
using System.Text;

namespace Robot_Controller
{
    internal class CalculateurDeplacement
    {

        private List<Commande> listeCommande;
        private Leap.Vector relativePosition;

        public CalculateurDeplacement()
        {
            listeCommande = new List<Commande>();
            relativePosition = new Leap.Vector();
        }


        public List<Commande> CalculDeplacementQuatreAxe(HandLeap startPosition, HandLeap actualPosition)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;
            Console.WriteLine(relativePosition.x);

            if (relativePosition.x < -50 && !listeCommande.Contains(Commande.Gauche))
            {
                listeCommande.Remove(Commande.Droite);
                listeCommande.Add(Commande.Gauche);
            }
            
            if (relativePosition.x > 50 && !listeCommande.Contains(Commande.Droite))
            {
                listeCommande.Remove(Commande.Gauche);
                listeCommande.Add(Commande.Droite);
            }

            if(relativePosition.y < -50 && !listeCommande.Contains(Commande.Haut))
            {
                listeCommande.Remove(Commande.Bas);
                listeCommande.Add(Commande.Haut);
            }
            
            if(relativePosition.y > 50 && !listeCommande.Contains(Commande.Bas))
            {
                listeCommande.Remove(Commande.Haut);
                listeCommande.Add(Commande.Bas);
            }

            if (listeCommande.Contains(Commande.Gauche))
                Console.WriteLine("GAUCHE");

            return listeCommande;
        }

    }
}
