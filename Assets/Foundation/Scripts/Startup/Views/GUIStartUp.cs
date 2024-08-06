using Foundation.Scripts.Coin.Views;
using Foundation.Scripts.WalletSystem.Models;
using Foundation.Scripts.WalletSystem.Presenters;
using Foundation.Scripts.WalletSystem.Views;
using System;
using UnityEngine;

namespace Foundation.Scripts.Startup.Views
{
    internal class GUIStartUp : MonoBehaviour
    {
        [SerializeField] private Transform _coinsContainer;
        [SerializeField] private WalletView _walletView;

        private WalletPresenter _walletPresenter;

        private IDisposable[] _disposables;

        private void Awake()
        {
            var coins = _coinsContainer.GetComponentsInChildren<CoinView>();

            _walletView.Initialize();
            
            WalletModel wallet = new WalletModel();
            _walletPresenter = new WalletPresenter(wallet, _walletView, coins);

            InitializeDisposables();
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        private void InitializeDisposables()
        {
            _disposables = new IDisposable[]
            {
                _walletPresenter
            };
        }
    }
}