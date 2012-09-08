using System.ComponentModel;

namespace LifeInMetro
{
    class LifeViewModel : INotifyPropertyChanged
    {
        private int generation;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Generation
        {
            get
            {
                return generation;
            }

            set
            {
                generation = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Generation"));
                }
            }
        }
    }
}
