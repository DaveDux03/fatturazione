using System;
using System.Collections.Generic;

namespace SistemaFatturazione
{
    class Program
    {

        //ciao 
        static List<Cliente> clienti = new List<Cliente>();
        static List<Prodotto> prodotti = new List<Prodotto>();
        static Cliente clienteCorrente = null;
        static List<Prodotto> carrello = new List<Prodotto>();

        static void Main(string[] args)
        {
            prodotti.Add(new Prodotto("1", "Mouse USB", 10.00m));
            prodotti.Add(new Prodotto("2", "Tastiera Wireless", 25.00m));
            prodotti.Add(new Prodotto("3", "Monitor 24 pollici", 150.00m));
            prodotti.Add(new Prodotto("4", "Cuffie Bluetooth", 45.00m));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========= Sistema di Fatturazione Semplificato =========");
                Console.WriteLine("1. Registrati");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Uscita");
                Console.Write("Seleziona un'opzione: ");
                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        Registrazione();
                        break;

                    case "2":
                        Login();
                        break;

                    case "3":
                        Console.WriteLine("Uscita in corso...");
                        return;

                    default:
                        Console.WriteLine("Opzione non valida, riprova...");
                        break;
                }
            }
        }

        static void Registrazione()
        {
            Console.Clear();
            Console.WriteLine("------ Registrazione Cliente ------");
            Console.Write("Inserisci ID cliente: ");
            string idCliente = Console.ReadLine();
            Console.Write("Inserisci Nome: ");
            string nomeCliente = Console.ReadLine();
            Console.Write("Inserisci Partita IVA: ");
            string partitaIVA = Console.ReadLine();
            Console.Write("Inserisci Indirizzo: ");
            string indirizzoCliente = Console.ReadLine();

            Cliente nuovoCliente = new Cliente(idCliente, nomeCliente, partitaIVA, indirizzoCliente);
            clienti.Add(nuovoCliente);
            clienteCorrente = nuovoCliente;

            Console.WriteLine("Registrazione completata con successo!");
            Console.WriteLine("Benvenuto, " + clienteCorrente.Nome + "!");
            Console.WriteLine("Procediamo alla fase di acquisto...\n");

            FaseAcquisto();
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("------ Login Cliente ------");
            Console.Write("Inserisci ID cliente: ");
            string idCliente = Console.ReadLine();

            clienteCorrente = clienti.Find(c => c.Id == idCliente);

            if (clienteCorrente == null)
            {
                Console.WriteLine("Cliente non trovato. Assicurati di aver inserito l'ID corretto.");
                return;
            }

            Console.WriteLine("Login effettuato con successo!");
            Console.WriteLine("Benvenuto, " + clienteCorrente.Nome + "!");
            Console.WriteLine("Procediamo alla fase di acquisto...\n");

            FaseAcquisto();
        }

        static void FaseAcquisto()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("------ Seleziona i Prodotti ------");
                StampaTabellaProdotti();
                Console.WriteLine("----------------------------------------");
                Console.Write("Inserisci l'ID del prodotto che desideri acquistare (o 'fine' per terminare): ");
                string idProdotto = Console.ReadLine();

                if (idProdotto.ToLower() == "fine")
                {
                    Console.WriteLine("Fase di acquisto terminata.");
                    break;
                }

                Prodotto prodottoSelezionato = prodotti.Find(p => p.Id == idProdotto);

                if (prodottoSelezionato == null)
                {
                    Console.WriteLine("Prodotto non trovato. Riprova...");
                    continue;
                }

                Console.Write("Inserisci la quantità: ");
                int quantita;

                while (!int.TryParse(Console.ReadLine(), out quantita) || quantita <= 0)
                {
                    Console.Write("Quantità non valida. Inserisci una quantità positiva: ");
                }

                for (int i = 0; i < quantita; i++)
                {
                    carrello.Add(prodottoSelezionato);
                }

                Console.WriteLine($"Hai aggiunto {quantita} x {prodottoSelezionato.Descrizione} al tuo carrello.");
                Console.WriteLine("Continua a selezionare prodotti o digita 'fine' per terminare.");
            }

            MostraFattura();
        }

        static void StampaTabellaProdotti()
        {
            Console.WriteLine("ID   Descrizione               Prezzo");
            Console.WriteLine("----------------------------------------");

            foreach (var prodotto in prodotti)
            {
                Console.WriteLine($"{prodotto.Id,-5} {prodotto.Descrizione,-25} ${prodotto.PrezzoUnitario:F2}");
            }
        }

        static void MostraFattura()
        {
            if (carrello.Count == 0)
            {
                Console.WriteLine("Il carrello è vuoto. Nessun acquisto effettuato.");
                return;
            }

            decimal totale = 0;
            Console.Clear();
            Console.WriteLine("========= Fattura =========");
            Console.WriteLine("Prodotto                        Quantità   Prezzo Unitario   Totale");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (var prodotto in carrello)
            {
                Console.WriteLine($"{prodotto.Descrizione,-30} 1       ${prodotto.PrezzoUnitario:F2}        ${prodotto.PrezzoUnitario:F2}");
                totale += prodotto.PrezzoUnitario;
            }

            decimal iva = totale * 0.22m;
            decimal totaleConIva = totale + iva;

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine($"Totale netto:                             ${totale:F2}");
            Console.WriteLine($"IVA (22%):                                ${iva:F2}");
            Console.WriteLine($"Totale da pagare:                        ${totaleConIva:F2}");
            Console.WriteLine("===============================");

            Console.WriteLine("\nPremi un tasto per tornare al menu principale.");
            Console.ReadKey();
        }
    }

    class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string PartitaIVA { get; set; }
        public string Indirizzo { get; set; }

        public Cliente(string id, string nome, string partitaIVA, string indirizzo)
        {
            Id = id;
            Nome = nome;
            PartitaIVA = partitaIVA;
            Indirizzo = indirizzo;
        }
    }

    class Prodotto
    {
        public string Id { get; set; }
        public string Descrizione { get; set; }
        public decimal PrezzoUnitario { get; set; }

        public Prodotto(string id, string descrizione, decimal prezzoUnitario)
        {
            Id = id;
            Descrizione = descrizione;
            PrezzoUnitario = prezzoUnitario;
            //test
        }
    }
}
