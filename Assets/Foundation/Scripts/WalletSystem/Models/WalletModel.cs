using System;

namespace Foundation.Scripts.WalletSystem.Models
{
    public class WalletModel
    {
        private int _currentCoinCount;

        public event Action<int> CoinsCountChanged;

        public WalletModel()
        {
            _currentCoinCount = 0;
        }

        public void AddCoins(int value)
        {
            _currentCoinCount += value;

            CoinsCountChanged?.Invoke(_currentCoinCount);
        }
    }
}