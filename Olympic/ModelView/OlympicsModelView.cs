using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympic.Controller;
using Olympic.Model;
using System.Collections.ObjectModel;

namespace Olympic.ModelView
{
    class OlympicsModelView : BaseModelView
    {
        private List<OlympicsModel> _lista_atleti;
        public List<OlympicsModel> Lista_Atleti
        {
            get { return _lista_atleti; }
            set { _lista_atleti = value; NotifyPropertyChanged("Lista_Atleti"); }
        }

        private ObservableCollection<string> _lista_sessi;
        public ObservableCollection<string> Lista_Sessi
        {
            get { return _lista_sessi; }
            set { _lista_sessi = value; NotifyPropertyChanged("Lista_Sessi"); }
        }

        private List<string> _lista_giochi;

        public List<string> Lista_Giochi
        {
            get { return _lista_giochi; }
            set { _lista_giochi = value; NotifyPropertyChanged("Lista_Giochi"); }
        }

        private List<string> _lista_medal;
        public List<string> Lista_Medal
        {
            get { return _lista_medal; }
            set { _lista_medal = value; NotifyPropertyChanged("Lista_Medal"); }
        }

        private string scelta_sex;
        public string Scelta_Sex
        {
            get { return scelta_sex; }
            set { scelta_sex = value; Pagina_Corrente = 1; NotifyPropertyChanged("Scelta_Sex"); RicaricaTabella(); }
        }

        private string scelta_name;
        public string Scelta_Name
        {
            get { return scelta_name; }
            set { scelta_name = value; Pagina_Corrente = 1; NotifyPropertyChanged("Scelta_Name"); RicaricaTabella(); }
        }

        private string scelta_medal;
        public string Scelta_Medal
        {
            get { return scelta_medal; }
            set { scelta_medal = value; Pagina_Corrente = 1; NotifyPropertyChanged("Scelta_Medal"); RicaricaTabella(); }

        }

        private string scelta_games;
        public string Scelta_Games
        {
            get { return scelta_games; }
            set { scelta_games = value; Pagina_Corrente = 1; NotifyPropertyChanged("Scelta_Games"); RicaricaTabella(); CaricaSport(); }
        }

        private List<string> lista_sport;
        public List<string> Lista_Sport
        {
            get { return lista_sport; }
            set { lista_sport = value; NotifyPropertyChanged("Lista_Sport"); }
        }

        private string scelta_sport;
        public string Scelta_Sport
        {
            get { return scelta_sport; }
            set { scelta_sport = value; Pagina_Corrente = 1; NotifyPropertyChanged("Scelta_Sport"); RicaricaTabella(); CaricaEvent(); }
        }

        private List<string> lista_event;
        public List<string> Lista_Event
        {
            get { return lista_event; }
            set { lista_event = value; NotifyPropertyChanged("Lista_Event"); }
        }

        private string scelta_event;
        public string Scelta_Event
        {
            get { return scelta_event; }
            set { scelta_event = value; Pagina_Corrente = 1; NotifyPropertyChanged("Scelta_Event"); RicaricaTabella(); }
        }

        private List<int> righe_perpag;
        public List<int> Righe_PerPag
        {
            get { return righe_perpag; }
            set { righe_perpag = value; NotifyPropertyChanged("Righe_PerPag"); }
        }

        private int scelta_righe;
        public int Scelta_Righe
        {
            get { return scelta_righe; }
            set { scelta_righe = value; NotifyPropertyChanged("Scelta_Righe"); RicaricaTabella(); }
        }

        private bool avanti;
        public bool Avanti
        {
            get { return avanti; }
            set { avanti = value; NotifyPropertyChanged("Avanti"); }
        }

        private bool indietro;
        public bool Indietro
        {
            get { return indietro; }
            set { indietro = value; NotifyPropertyChanged("Indietro"); }
        }

        private int pagine_totali;
        public int Pagine_Totali
        {
            get { return pagine_totali; }
            set { pagine_totali = value; Impaginazione_(); }
        }

        public int Pagina_DiRiferimento;

        private int pagina_corrente = 1;
        public int Pagina_Corrente
        {
            get { return pagina_corrente; }
            set { pagina_corrente = value; Impaginazione_(); }
        }

        private string impaginazione;
        public string Impaginazione
        {
            get { return impaginazione; }
            set {  impaginazione= value; NotifyPropertyChanged("Impaginazione");  }
        }

        public OlympicsModelView()
        {
            Lista_Atleti = OlympicsController.GetAll();
            Lista_Sessi = new ObservableCollection<string>(OlympicsController.GetSex());
            Lista_Giochi = OlympicsController.GetGames();
            Lista_Medal = OlympicsController.GetMedal();
            Righe_PerPag = OlympicsController.GetRighe();
            Scelta_Righe = 10;
       
        }


        public void Impaginazione_()
        {
            Impaginazione = $"Pagina n° {Pagina_Corrente} di {Pagine_Totali} Totali"; 
        }
        public void RicaricaTabella()
        {   
           
            Lista_Atleti = OlympicsController.Find(Scelta_Sex, Scelta_Medal, Scelta_Games, Scelta_Name, Scelta_Sport, Scelta_Event, Pagina_Corrente, Scelta_Righe, ref Pagina_DiRiferimento);
            Pagine_Totali = Pagina_DiRiferimento / Scelta_Righe;
            Impaginazione_();
            if (Pagina_Corrente == Pagine_Totali)
            {
                Avanti = false;
                Indietro = true;
            }
            if (Pagina_Corrente == 1)
            {
                Avanti = true;
                Indietro = false; 
            }
            if (Pagina_Corrente > 1 && Pagina_Corrente < Pagine_Totali)
            {
                Avanti = true;
                Indietro = false; 
            }

            
        }
        public void CaricaSport()
        {
            Lista_Sport = OlympicsController.GetSport(Scelta_Games);
        }
        public void CaricaEvent()
        {
            Lista_Event = OlympicsController.GetEvent(Scelta_Games, Scelta_Sport);
        }

        public void AzzeraFiltri()
        { 
            Lista_Atleti = OlympicsController.GetAll();
            Scelta_Sex = null;
            Scelta_Medal = null;
            Scelta_Event = null;
            Scelta_Games = null;
            Scelta_Sport = null;
            Scelta_Name = null;
            Scelta_Righe = 10;
            Pagina_Corrente = 1;
            
        }

        public void VaiPagIniziale()
        {
            Pagina_Corrente = 1;
            RicaricaTabella();
        }
        public void VaiUltimaPag()
        {
            Pagina_Corrente = Pagine_Totali;
            RicaricaTabella();
        }
        public void VaiAvanti()
        {
            Pagina_Corrente++;
            RicaricaTabella();
        }
        public void VaiIndietro()
        {
            Pagina_Corrente--;
            RicaricaTabella();
        }
    }
}
