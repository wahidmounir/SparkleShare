//   SparkleShare, a collaboration and sharing tool.
//   Copyright (C) 2010  Hylke Bons <hi@planetpeanut.uk>
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Threading;

using Sparkles;

namespace SparkleShare {

    public partial class PageController {

        public void CheckCryptoSetupPage (string password)
        {
            new Thread (() => {
                bool is_valid_password = (password.Length > 0 && !password.StartsWith (" ") && !password.EndsWith (" "));
                PageCanContinueEvent (PageType.CryptoSetup, is_valid_password);
            }).Start ();
        }


        public void CheckCryptoPasswordPage (string password)
        {
            bool is_password_correct = SparkleShare.Controller.CheckPassword (password);
            PageCanContinueEvent (PageType.CryptoPassword, is_password_correct);
        }


        public void CryptoPageCompleted (string password)
        {
            ProgressBarPercentage = 100.0;
            ChangePageEvent (PageType.Progress);

            new Thread (() => {
                Thread.Sleep (page_delay);
                SparkleShare.Controller.FinishFetcher (StorageType.Encrypted, password);

            }).Start ();
        }
    }
}