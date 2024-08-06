using Foundation.Scripts.Coin.Views;
using Foundation.Scripts.WalletSystem.Models;
using Foundation.Scripts.WalletSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Scripts.WalletSystem.Presenters
{
    public class WalletPresenter : IDisposable
    {
        private WalletModel _walletModel;
        private WalletView _walletView;

        private List<CoinView> _coins;

        public WalletPresenter(WalletModel walletModel, WalletView walletView,
            ICollection<CoinView> coins)
        {
            _walletModel = walletModel;
            _walletView = walletView;
            _coins = coins.ToList();

            Initialize();
        }

        private void Initialize()
        {
            foreach (CoinView coinView in _coins)
            {
                coinView.Taken += OnCoinTaken;
            }

            _walletModel.CoinsCountChanged += OnCoinsCountChanged;
        }

        public void Dispose()
        {
            foreach (CoinView coinView in _coins)
            {
                coinView.Taken -= OnCoinTaken;
            }

            _walletModel.CoinsCountChanged -= OnCoinsCountChanged;
        }

        private void OnCoinTaken(int coinValue)
        {
            _walletModel.AddCoins(coinValue);
        }

        private void OnCoinsCountChanged(int coinsCount)
        {
            _walletView.UpdateCoinsCount(coinsCount);
        }
    }
}