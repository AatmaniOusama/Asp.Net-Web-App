using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLockTooTaxi.MyDataSetTableAdapters;

namespace DataLockTooTaxi
{
    public class Events
    {
        public int NumLecteur { get; set; }
        public string Instant { get; set; }
        public int NumEvent { get; set; }
        public int Type { get; set; }
        public int Sous_Type { get; set; }
        public string Reference { get; set; }
        public bool Traite { get; set; }
        public int EntreeSortie { get; set; }
        public bool Valide { get; set; }
        public string Code { get; set; }
        public int IdEvent { get; set; }
        public string NumAgrement { get; set; }
        public string CodeRefus { get; set; }
        public string MatriculeAdmin { get; set; }
        public string NumTaxi { get; set; }
        public int TypeTaxi { get; set; }


        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NumBadge { get; set; }               
        public string LibelleTypeTaxi { get; set; }        
        public string LibelleCode { get; set; }
        public int TypeCode { get; set; }
        public string LibelleTypeCode { get; set; }      
        public string NomAgent { get; set; }
        public string PrenomAgent { get; set; }               
        public string LibelleModePointage { get; set; }
        public string NomLecteur { get; set; }
        public string Immatriculation { get; set; }


        public Events(string numTaxi,string numBadge)
        {
            NumTaxi = numTaxi;
            NumBadge = numBadge;
        }

        public Events()
        { 
            NumLecteur = -1;
            Instant =DateTime.Now.ToString().Substring(0,16);
            NumEvent = -1;
            Type = -1;
            Sous_Type = -1;
            Reference  = "";
            Traite = false;
            EntreeSortie = 0;
            Valide = true;
            Code = "";
            IdEvent = -1;
            NumAgrement = "";
            CodeRefus = ""; 
            MatriculeAdmin = "";
            NumTaxi = "";
            TypeTaxi = -1;
        }

        public List<Events> GetEvents()
        {
            EventsTableAdapter EventAdapter = new EventsTableAdapter();
            MyDataSet.EventsDataTable TableEvents = EventAdapter.GetEvents();
            List<Events> ListEvents = new List<Events>();

            foreach (MyDataSet.EventsRow row in TableEvents)
            {
                Events item = new Events();

                item.IdEvent = row.IdEvent;


                if (!row.IsInstantNull())
                item.Instant = row.Instant.ToString().Substring(0, 16);

                if (!row.IsReferenceNull())
                item.Reference = row.Reference;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsNumTaxiNull())
                    item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsLibelleTypeTaxiNull())
                    item.LibelleTypeTaxi = row.LibelleTypeTaxi;

                if (!row.IsCodeRefusNull())
                    item.CodeRefus = row.CodeRefus;

                if (!row.IsLibelleCodeNull())
                    item.LibelleCode = row.LibelleCode;

                if (!row.IsTypeCodeNull())
                {
                    item.TypeCode = row.TypeCode;

                    switch (row.TypeCode)
                    {
                        case 1:
                            item.LibelleTypeCode = "Code Refus";
                            break;

                        case 2:
                            item.LibelleTypeCode = "Code Acceptation";
                            break;

                        default:
                            break;
                    }
                }

                if (!row.IsMatriculeAdminNull())
                    item.MatriculeAdmin = row.MatriculeAdmin;

                if (!row.IsNomAgentNull())
                    item.NomAgent = row.NomAgent;

                if (!row.IsPrenomAgentNull())
                    item.PrenomAgent = row.PrenomAgent;

                if (!row.IsNumEventNull())
                item.NumEvent = row.NumEvent;

                if (!row.IsTypeNull())
                    item.Type = row.Type;

                if (!row.IsSous_TypeNull())
                {
                    item.Sous_Type = row.Sous_Type;

                    switch (row.Sous_Type)
                    {
                        case 141:
                            item.LibelleModePointage = "Doigt";
                            break;
                        case 142:
                            item.LibelleModePointage = "Badge";
                            break;
                        case 144:
                            item.LibelleModePointage = "Badge et Doigt";
                            break;
                        case 48:
                            item.LibelleModePointage = "Saisi";
                            break;
                        default:
                            break;

                    }
                }

                if (!row.IsTraiteNull())
                item.Traite = row.Traite;

                if (!row.IsEntreeSortieNull())
                item.EntreeSortie = row.EntreeSortie;

                if (!row.IsValideNull())
                item.Valide = row.Valide;

                if (!row.IsCodeNull())
                item.Code = row.Code;

             
                

                if (!row.IsNumLecteurNull())
                item.NumLecteur = row.NumLecteur;

                if (!row.IsNomLecteurNull())
                item.NomLecteur = row.NomLecteur;

                if (!row.IsPlaqueNull())
                    item.Immatriculation = row.Plaque;
                else
                    item.Immatriculation = "";

                ListEvents.Add(item);
            }

            return ListEvents;
        }
       

        /***************************************************************/
        /*               Get  Events de pointage                */
        /***************************************************************/
        public List<Events> getEventsByFiltre(int Index, DateTime? dateDebut, DateTime? dateFin, string matricule, string nom, string numPermis, string immatriculation, string numTaxi, int? typeTaxi, string codeRefus, string matriculeAgent, int? typeCode, int? sous_Type, int? numLecteur)
        {

            EventsTableAdapter EventAdapter = new EventsTableAdapter();

          


             MyDataSet.EventsDataTable TableEvents = EventAdapter.GetEventsByFiltre(Index, dateDebut, dateFin, matricule, nom, numPermis, immatriculation, numTaxi, typeTaxi, codeRefus, matriculeAgent, typeCode, sous_Type, numLecteur);
                  
             
           

            List<Events> ListEvents = new List<Events>();

            foreach (MyDataSet.EventsRow row in TableEvents)
            {
                Events item = new Events();

                item.IdEvent = row.IdEvent;


                if (!row.IsInstantNull())
                    item.Instant = row.Instant.ToString().Substring(0, 16);

                if (!row.IsReferenceNull())
                    item.Reference = row.Reference;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsNumTaxiNull())
                    item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsLibelleTypeTaxiNull())
                    item.LibelleTypeTaxi = row.LibelleTypeTaxi;

                if (!row.IsCodeRefusNull())
                    item.CodeRefus = row.CodeRefus;

                if (!row.IsLibelleCodeNull())
                    item.LibelleCode = row.LibelleCode;

                if (!row.IsTypeCodeNull())
                {
                    item.TypeCode = row.TypeCode;

                    switch (row.TypeCode)
                    {
                        case 1:
                            item.LibelleTypeCode = "Code Refus";
                            break;

                        case 2:
                            item.LibelleTypeCode = "Code Acceptation";
                            break;

                        default:
                            break;
                    }
                }

                if (!row.IsMatriculeAdminNull())
                    item.MatriculeAdmin = row.MatriculeAdmin;

                if (!row.IsNomAgentNull())
                    item.NomAgent = row.NomAgent;

                if (!row.IsPrenomAgentNull())
                    item.PrenomAgent = row.PrenomAgent;

                if (!row.IsNumEventNull())
                    item.NumEvent = row.NumEvent;

                if (!row.IsTypeNull())
                    item.Type = row.Type;

                if (!row.IsSous_TypeNull())
                {
                    item.Sous_Type = row.Sous_Type;

                    switch (row.Sous_Type)
                    {
                        case 141:
                            item.LibelleModePointage = "Doigt";
                            break;
                        case 142:
                            item.LibelleModePointage = "Badge";
                            break;
                        case 144:
                            item.LibelleModePointage = "Badge et Doigt";
                            break;
                        case 48:
                            item.LibelleModePointage = "Saisi";
                            break;
                        default:
                            break;

                    }
                }

                if (!row.IsTraiteNull())
                    item.Traite = row.Traite;

                if (!row.IsEntreeSortieNull())
                    item.EntreeSortie = row.EntreeSortie;

                if (!row.IsValideNull())
                    item.Valide = row.Valide;

                if (!row.IsCodeNull())
                    item.Code = row.Code;

                if (!row.IsPlaqueNull())
                    item.Immatriculation = row.Plaque;
                else
                    item.Immatriculation = "";


                if (!row.IsNumLecteurNull())
                    item.NumLecteur = row.NumLecteur;

                if (!row.IsNomLecteurNull())
                    item.NomLecteur = row.NomLecteur;

                ListEvents.Add(item);
            }

            return ListEvents;
        }

        /***************************************************************/
        /*               Get  Events de pointage   pour l'Export              */
        /***************************************************************/


        /***************************************************************/
        /*               Get  Events de pointage                */
        /***************************************************************/
        public List<Events> getAllEvents(DateTime? dateDebut, DateTime? dateFin, string matricule, string nom, string numPermis, string immatriculation, string numTaxi, int? typeTaxi, string codeRefus, string matriculeAgent, int? typeCode, int? sous_Type, int? numLecteur)
        {

            EventsTableAdapter EventAdapter = new EventsTableAdapter();




            MyDataSet.EventsDataTable TableEvents = EventAdapter.GetAllEvents(dateDebut, dateFin, matricule, nom, numPermis, immatriculation, numTaxi, typeTaxi, codeRefus, matriculeAgent, typeCode, sous_Type, numLecteur);




            List<Events> ListEvents = new List<Events>();

            foreach (MyDataSet.EventsRow row in TableEvents)
            {
                Events item = new Events();

                item.IdEvent = row.IdEvent;


                if (!row.IsInstantNull())
                    item.Instant = row.Instant.ToString().Substring(0, 16);

                if (!row.IsReferenceNull())
                    item.Reference = row.Reference;

                if (!row.IsNomNull())
                    item.Nom = row.Nom;

                if (!row.IsPrenomNull())
                    item.Prenom = row.Prenom;

                if (!row.IsNumBadgeNull())
                    item.NumBadge = row.NumBadge;

                if (!row.IsNumAgrementNull())
                    item.NumAgrement = row.NumAgrement;

                if (!row.IsNumTaxiNull())
                    item.NumTaxi = row.NumTaxi;

                if (!row.IsTypeTaxiNull())
                    item.TypeTaxi = row.TypeTaxi;

                if (!row.IsLibelleTypeTaxiNull())
                    item.LibelleTypeTaxi = row.LibelleTypeTaxi;

                if (!row.IsCodeRefusNull())
                    item.CodeRefus = row.CodeRefus;

                if (!row.IsLibelleCodeNull())
                    item.LibelleCode = row.LibelleCode;

                if (!row.IsTypeCodeNull())
                {
                    item.TypeCode = row.TypeCode;

                    switch (row.TypeCode)
                    {
                        case 1:
                            item.LibelleTypeCode = "Code Refus";
                            break;

                        case 2:
                            item.LibelleTypeCode = "Code Acceptation";
                            break;

                        default:
                            break;
                    }
                }

                if (!row.IsMatriculeAdminNull())
                    item.MatriculeAdmin = row.MatriculeAdmin;

                if (!row.IsNomAgentNull())
                    item.NomAgent = row.NomAgent;

                if (!row.IsPrenomAgentNull())
                    item.PrenomAgent = row.PrenomAgent;

                if (!row.IsNumEventNull())
                    item.NumEvent = row.NumEvent;

                if (!row.IsTypeNull())
                    item.Type = row.Type;

                if (!row.IsSous_TypeNull())
                {
                    item.Sous_Type = row.Sous_Type;

                    switch (row.Sous_Type)
                    {
                        case 141:
                            item.LibelleModePointage = "Doigt";
                            break;
                        case 142:
                            item.LibelleModePointage = "Badge";
                            break;
                        case 144:
                            item.LibelleModePointage = "Badge et Doigt";
                            break;
                        case 48:
                            item.LibelleModePointage = "Saisi";
                            break;
                        default:
                            break;

                    }
                }

                if (!row.IsTraiteNull())
                    item.Traite = row.Traite;

                if (!row.IsEntreeSortieNull())
                    item.EntreeSortie = row.EntreeSortie;

                if (!row.IsValideNull())
                    item.Valide = row.Valide;

                if (!row.IsCodeNull())
                    item.Code = row.Code;

                if (!row.IsPlaqueNull())
                    item.Immatriculation = row.Plaque;
                else
                    item.Immatriculation = "";


                if (!row.IsNumLecteurNull())
                    item.NumLecteur = row.NumLecteur;

                if (!row.IsNomLecteurNull())
                    item.NomLecteur = row.NomLecteur;

                ListEvents.Add(item);
            }

            return ListEvents;
        }

      
        /************************************************/
        /*            Ajouter Un Event           */
        /************************************************/

        public int AjouterUnEvent(Events item)
        {
            EventsTableAdapter EventsAdapter = new EventsTableAdapter();

            return EventsAdapter.InsertEvents(item.NumLecteur, DateTime.Now, 0, 59, 48, item.Reference, false, 0, true, "",item.NumTaxi, item.CodeRefus, item.MatriculeAdmin, item.NumTaxi, item.TypeTaxi);
           

        }


        /************************************************/
        /*           Supprimer Un Event           */
        /************************************************/

        public int SupprimerUnEvent(int IdEvent)
        {
            EventsTableAdapter EventsAdapter = new EventsTableAdapter();
            return EventsAdapter.DeleteEvent(IdEvent);
        }


        /************************************************/
        /*                  Max IdEvent                  */
        /************************************************/

        public int MaxIdEvent()
        {
            EventsTableAdapter EventsAdapter = new EventsTableAdapter();
            try
            {
                return EventsAdapter.GetMaxIdEvent().Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /************************************************/
        /*           Get Reference d'un Utilisateur           */
        /************************************************/

        public string GetReferenceByNumPermis(string numBadge)
        {
            EventsTableAdapter EventsAdapter = new EventsTableAdapter();
            return EventsAdapter.GetReferenceByNumPermis(numBadge).ToString();
        }
        
        /***************************************************************/
        /*               Count des Events de pointage                 */
        /***************************************************************/

        public int getTotalEvents()
        {
            EventsTableAdapter EventsAdapter = new EventsTableAdapter();
            try
            {
                return EventsAdapter.GetTotalEvents().Value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /*******************************************************************************/
        /*               Total des Events   Avec Filtre        */
        /******************************************************************************/

        public int getTotalEventsByFiltre( DateTime dateDebut, DateTime dateFin, string matricule, string nom, string numPermis, string immatriculation, string numTaxi,int? typeTaxi,string codeRefus, string matriculeAgent, int? typeCode,int? sous_Type,int? numLecteur)
        {

            EventsTableAdapter EventsAdapter = new EventsTableAdapter();

            try
            {
                return int.Parse(EventsAdapter.GetTotalEventsByFiltre(dateDebut, dateFin, matricule, nom, numPermis, immatriculation, numTaxi, typeTaxi, codeRefus, matriculeAgent, typeCode, sous_Type, numLecteur).ToString());
            }
            catch (Exception)
            {
                return 0;
            }


        }



        public List<Events> GetSyntheseControles(int TypeTaxi,bool valide,DateTime DateDebut,DateTime dateFin)
        {
            SyntheseTableAdapter SyntheseAdapter = new SyntheseTableAdapter();
            AgrementTableAdapter AgrementAdapter=new AgrementTableAdapter();
            List<Events> ListEvents=new List<Events>();

            int DernierNumTaxi=int.Parse(AgrementAdapter.DernierNumTaxi(TypeTaxi).ToString());
            for (int i = 1; i <= DernierNumTaxi; i++)
            { 
                ListEvents.Add(new Events(i.ToString()," "));
            }
            foreach (MyDataSet.SyntheseRow row in SyntheseAdapter.GetSyntheseControles(TypeTaxi, valide, DateDebut, dateFin))
            {
                if (!row.IsNumTaxiNull() && !row.IsNumBadgeNull() && !row.Isinstant1Null())
                {
                    try
                    {
                        if (ListEvents[int.Parse(row.NumTaxi) - 1].NumBadge == " ")
                            ListEvents[int.Parse(row.NumTaxi) - 1].NumBadge = row.NumBadge.Substring(6, 4);

                        else if (!ListEvents[int.Parse(row.NumTaxi) - 1].NumBadge.Contains("X"))
                            ListEvents[int.Parse(row.NumTaxi) - 1].NumBadge = "XX";

                        else
                            ListEvents[int.Parse(row.NumTaxi) - 1].NumBadge += "X";
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return ListEvents;
        }

        public int NbrPointages(string Matricule)
        {
            EventsTableAdapter EventAdapter = new EventsTableAdapter();

            return EventAdapter.CountEvents(Matricule).Value;
        }
    }

}
