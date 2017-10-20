namespace Examples
{
    public class Urzadzenie
    {
        private int? _czesciRuchomych;
        public int? CzesciRuchomych
        {
            get { return _czesciRuchomych; }
            set { _czesciRuchomych = value; }
        }

        private int? _czesciStalych;
        public int? CzesciStalych
        {
            get { return _czesciStalych; }
            set { _czesciStalych = value; }
        }

        private int? _czesciOgolem;
        public int? CzesciOgolem
        {
            /*
             *  get
            {
                if (czesciOgolem != null)
                {
                    return czesciOgolem;
                }
                else if (czesciRuchomych != null || czesciStalych != null)
                {
                    int? ret = czesciRuchomych;
                    if (czesciStalych != null)
                    {
                        if (czesciRuchomych != null)
                        {
                            ret += czesciStalych;
                        }
                        else
                        {
                            ret = czesciStalych;
                        }
                        return ret;
                    }
                    return ret;
                }

                return null;
            }
             * */

            get
            {
                if (_czesciOgolem != null)
                {
                    return _czesciOgolem;
                }

                if (_czesciStalych != null && _czesciRuchomych != null)
                {
                    return _czesciRuchomych + _czesciStalych;
                }

                return _czesciStalych ?? _czesciRuchomych;
            }
            set { _czesciOgolem = value; }
        }
    }
}