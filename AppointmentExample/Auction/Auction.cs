using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentExample.Auction
{
    public class Auction
    {

        private decimal _startPrice;
        private Car _car;
        private Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _whenToSell;
        private Thread _auctionDisplayTimeTh;
        private Thread _auctionObserverTh;
        private Dictionary<string, decimal> _opponents = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> _displayOpponents = new Dictionary<string, decimal>();
        private object _auctionLock = new object();
        private bool _auctionStarted = false;

        public Auction(Car car, decimal startPrice, TimeSpan whenToSell)
        {
            _startPrice = startPrice;
            _car = car;
            _whenToSell = whenToSell;
            Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
            _auctionDisplayTimeTh = new Thread(AuctionInfoChecker);
            _auctionObserverTh = new Thread(AuctionObserver);
        }
        public void StartAuction()
        {
            //must be ordered like that!!!

            _auctionStarted = true;
            _stopwatch.Start();
            _auctionDisplayTimeTh.Start();
            _auctionObserverTh.Start();
        }

        //Timer for auction
        private void AuctionObserver()
        {
            while (_auctionStarted)
            {
                if (_stopwatch.Elapsed >= _whenToSell)
                {
                    _auctionStarted = false;
                    Console.WriteLine($"Auction ended. Winner: {_opponents.LastOrDefault().Key}, Price: {_opponents.LastOrDefault().Value}");
                    AuctionReset();
                }
                Thread.Sleep(100);
            }
        }

        
        public void AuctionInfoChecker()
        {
            while (_auctionStarted)
            {
                if (_opponents.Count > 0)
                {
                    Console.WriteLine($"Last Bidder: {_displayOpponents.LastOrDefault().Key}, Last Bid: {_displayOpponents.LastOrDefault().Value}");
                }
                Thread.Sleep(1000);
            }
        }

        //for request
        public void AddOffer(string user, decimal amount)
        {
            lock (_auctionLock)
            {
                if (_auctionStarted && _stopwatch.Elapsed < _whenToSell)
                {
                    try
                    {
                        _opponents.Add(user, _startPrice + amount);
                        _displayOpponents.Add(user, _startPrice + amount);
                    }
                    catch (ArgumentException)
                    {
                        _opponents[user] = _startPrice + amount;
                        _displayOpponents[user] = _startPrice + amount;
                    }
                    _startPrice += amount;
                    _stopwatch.Restart();
                    Console.WriteLine($"{user}'s bid added successfully. Current price: {_startPrice}");
                    Thread.Sleep(2000); //bidding time 2s
                }
                else
                {
                    Console.WriteLine("Auction is not running or has ended.");
                }
            }

        }

        public void AuctionReset()
        {
            _opponents.Clear();
            _displayOpponents.Clear();
            _stopwatch.Reset();
        }

    }
}
