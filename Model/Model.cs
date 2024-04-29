using Logic;
using System;
using System.Numerics;

namespace Model
{
    public class OnPositionChangeUiAdapterEventArgs : EventArgs
    {
        public readonly Vector2 Koordynaty;
        public readonly int ID;

        public OnPositionChangeUiAdapterEventArgs(Vector2 koordynaty, int id)
        {
            this.Koordynaty = koordynaty;
            ID = id;
        }
    }

    public class ModelClass
    {
        public double SzerokoscPlanszy;
        public double WysokoscPlanszy;
        public int LiczbaKul;
        public LogicAPI? logika;
        public event EventHandler<OnPositionChangeUiAdapterEventArgs>? ZmianaKoordynatow;


        public ModelClass()
        {
            SzerokoscPlanszy = 600;
            WysokoscPlanszy = 600;
            logika = new BallController(SzerokoscPlanszy, WysokoscPlanszy);
            LiczbaKul = 0;

            logika.PositionChangeEvent += (o, k) =>
            {
                ZmianaKoordynatow?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(k.getBall().Position, k.id));
            };
        }
        public void StartProgram()
        {
            logika.addBalls(LiczbaKul);
            logika.Start();
        }

        public void StopProgram()
        {
            logika.Stop();
            logika = new BallController(SzerokoscPlanszy, WysokoscPlanszy);
            logika.PositionChangeEvent += (z, k) =>
            {
                ZmianaKoordynatow?.Invoke(this, new OnPositionChangeUiAdapterEventArgs(k.getBall().Position, k.id));
            };
        }

        public void SetLicznikKul(int ilosc)
        {
            LiczbaKul = ilosc;
        }

        public int GetLicznikKul()
        {
            return LiczbaKul;
        }

        //public void OnBallPositionChange(OnPositionChangeUiAdapterEventArgs args)
        //{
        //    ZmianaKoordynatow?.Invoke(this, args);
        //}
    }
}
