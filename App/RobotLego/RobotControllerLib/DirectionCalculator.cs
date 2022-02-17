using System.Collections.Generic;

namespace RobotController
{
    public class DirectionCalculator
    {

        public List<Direction> ListeDirection { get; set; }
        public int Vitesse { get; set; }
        private Leap.Vector relativePosition;
        private static int marge = 60;
        private static int margeY = 50;

        public DirectionCalculator()
        {
            ListeDirection = new List<Direction>();
            relativePosition = new Leap.Vector();
        }


        public void Reset()
        {
            ListeDirection.Clear();
            Vitesse = 0;
        }


        public void CalculDeplacementQuatreAxe(HandLeap startPosition, HandLeap actualPosition)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;

            if (relativePosition.x < -marge && !ListeDirection.Contains(Direction.GAUCHE))
            {
                ListeDirection.Add(Direction.GAUCHE);
            }

            if (relativePosition.x > marge && !ListeDirection.Contains(Direction.DROITE))
            {
                ListeDirection.Add(Direction.DROITE);
            }

            if (relativePosition.z < -marge && !ListeDirection.Contains(Direction.HAUT))
            {
                ListeDirection.Add(Direction.HAUT);
            }

            if (relativePosition.z > marge && !ListeDirection.Contains(Direction.BAS))
            {
                ListeDirection.Add(Direction.BAS);
            }

            if (relativePosition.x >= -marge && relativePosition.x <= marge && (ListeDirection.Contains(Direction.GAUCHE) || ListeDirection.Contains(Direction.DROITE)))
            {
                ListeDirection.Remove(Direction.GAUCHE);
                ListeDirection.Remove(Direction.DROITE);
            }

            if (relativePosition.z >= -marge && relativePosition.z <= marge && (ListeDirection.Contains(Direction.HAUT) || ListeDirection.Contains(Direction.BAS)))
            {
                ListeDirection.Remove(Direction.HAUT);
                ListeDirection.Remove(Direction.BAS);
            }
        }




        public void CalculRotation(HandLeap Position)
        {
            if (Position.Rotation.z > 0.3 && !ListeDirection.Contains(Direction.TOURNERGAUCHE))
            {
                ListeDirection.Add(Direction.TOURNERGAUCHE);
            }

            if (Position.Rotation.z < -0.4 && !ListeDirection.Contains(Direction.TOURNERDROITE))
            {
                ListeDirection.Add(Direction.TOURNERDROITE);
            }

            if (Position.Rotation.z <= 0.3 && Position.Rotation.z >= -0.4 && (ListeDirection.Contains(Direction.TOURNERGAUCHE) || ListeDirection.Contains(Direction.TOURNERDROITE)))
            {
                ListeDirection.Remove(Direction.TOURNERGAUCHE);
                ListeDirection.Remove(Direction.TOURNERDROITE);
            }
        }


        public void CalculVitesse(HandLeap startPosition, HandLeap actualPosition)
        {
            relativePosition = actualPosition.PalmPosition - startPosition.PalmPosition;

            if (relativePosition.y > margeY)
            {
                Vitesse = (int)relativePosition.y / 2 + 50 - margeY / 2;
            }

            if (relativePosition.y < -margeY)
            {
                Vitesse = (int)relativePosition.y / 2 + 50 + margeY / 2;
            }

            if (relativePosition.y <= margeY && relativePosition.y >= -margeY)
            {
                Vitesse = 50;
            }


            if (Vitesse < 10) Vitesse = 10;
            if (Vitesse > 100) Vitesse = 100;
        }
    }
}
