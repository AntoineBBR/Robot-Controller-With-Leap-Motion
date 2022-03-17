namespace RobotControllerLib
{
    /// <summary>
    /// all direction detect by the leap motion
    /// possible combination:
    ///     HAUT
    ///     HAUT DROITE
    ///     DROITE
    ///     DROITE BAS
    ///     BAS
    ///     BAS GAUCHE
    ///     GAUCHE
    ///     GAUCHE HAUT
    /// TOURNERGAUCHE / TOURNERDROITE can be add to any combination
    /// </summary>
    public enum Commande
    {
        HAUT,               //up
        BAS,                //down
        GAUCHE,             //left
        DROITE,             //right
        TOURNERGAUCHE,      //turn left
        TOURNERDROITE       //turn right
    }
}
