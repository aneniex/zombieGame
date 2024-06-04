using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace RapturePlay
{
    public class InAppManager : MonoBehaviour, IDetailedStoreListener
    {
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject messagePanel;
        [SerializeField] private GameObject removeAdsPanel;

        [SerializeField] private TextMeshProUGUI messageText;

        private IStoreController m_StoreController;
        private IExtensionProvider m_StoreExtensionProvider;

        public static InAppManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }
            else
            {
                Destroy(this.gameObject);
            }

            messagePanel.SetActive(false);
            loadingPanel.SetActive(false);
            removeAdsPanel.SetActive(false);
        }

        private void Start()
        {
#if UNITY_EDITOR
            StandardPurchasingModule.Instance().useFakeStoreAlways = true;
#endif
            InitIAP();

        }

        private void InitIAP()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(IAPItems.remove_ads, ProductType.NonConsumable);
            builder.AddProduct(IAPItems.unlock_characters, ProductType.NonConsumable);

            builder.AddProduct(IAPItems.coins_500, ProductType.Consumable);
            builder.AddProduct(IAPItems.coins_800, ProductType.Consumable);
            builder.AddProduct(IAPItems.coins_1000, ProductType.Consumable);
            builder.AddProduct(IAPItems.coins_1500, ProductType.Consumable);

            builder.AddProduct(IAPItems.gems_200, ProductType.Consumable);
            builder.AddProduct(IAPItems.gems_300, ProductType.Consumable);
            builder.AddProduct(IAPItems.gems_500, ProductType.Consumable);
            builder.AddProduct(IAPItems.gems_1000, ProductType.Consumable);

            builder.AddProduct(IAPItems.master_bundle, ProductType.Consumable);
            builder.AddProduct(IAPItems.mega_bundle, ProductType.Consumable);
            builder.AddProduct(IAPItems.legendary_bundle, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);

        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            this.m_StoreController = controller;
            this.m_StoreExtensionProvider = extensions;

            foreach (var item in controller.products.all)
            {
                Debug.Log(item.metadata.localizedTitle);
            }
        }

        public Product GetProductWithId(string productId)
        {
            return m_StoreController.products.WithID(productId);
        }

        public string GetProductPriceFromStore(string productId)
        {
            if (m_StoreController != null && m_StoreController.products != null)
            {
                return GetProductWithId(productId).metadata.localizedPriceString;
            }
            else
            {
                return "Price";
            }
        }

        public void OnPurchaseClicked(string id)
        {
            if (!IsInitialized()) return;
            m_StoreController.InitiatePurchase(id);

            loadingPanel.SetActive(true);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            throw new System.NotImplementedException();
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("Initialization Failed");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log("Purchase Failed");

            loadingPanel.SetActive(false);
            messagePanel.SetActive(true);
            messageText.text = "Purchase Failed";
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("Purchase Failed");

            loadingPanel.SetActive(false);
            messagePanel.SetActive(true);
            messageText.text = "Purchase Failed";
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.remove_ads, StringComparison.Ordinal))
            {
                PlayerPrefs.SetInt(PlayerPrefsHelper.RemoveAds, 1);

                if (AdsManager.Instance)
                {
                    AdsManager.Instance.DestroyBannerAd();
                }
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.unlock_characters, StringComparison.Ordinal))
            {
                for (int i = 0; i <= 7; i++)
                {
                    string playerString = $"Player_{i}";
                    PlayerPrefs.SetInt(playerString, 1);
                }
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.coins_500, StringComparison.Ordinal))
            {
                AddCoins(500);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.coins_800, StringComparison.Ordinal))
            {
                AddCoins(800);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.coins_1000, StringComparison.Ordinal))
            {
                AddCoins(1000);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.coins_1500, StringComparison.Ordinal))
            {
                AddCoins(1500);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.gems_200, StringComparison.Ordinal))
            {
                AddGems(200);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.gems_300, StringComparison.Ordinal))
            {
                AddGems(300);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.gems_500, StringComparison.Ordinal))
            {
                AddGems(500);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.gems_1000, StringComparison.Ordinal))
            {
                AddGems(1000);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.master_bundle, StringComparison.Ordinal))
            {
                AddCoins(2000);
                AddGems(200);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.mega_bundle, StringComparison.Ordinal))
            {
                AddCoins(3000);
                AddGems(300);
            }

            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, IAPItems.legendary_bundle, StringComparison.Ordinal))
            {
                AddCoins(5000);
                AddGems(500);
            }

            //TODO: Add rest of the products

            loadingPanel.SetActive(false);
            messagePanel.SetActive(true);
            messageText.text = "Purchase Successful";

            return PurchaseProcessingResult.Complete;
        }

        public void OnRestorPurhcases()
        {
            if (IsInitialized())
            {
                m_StoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnTransactionsRestored);

                loadingPanel.SetActive(true);
            }
        }

        private void OnTransactionsRestored(bool success, string msg)
        {
            if (success)
            {
                Debug.Log("Successfully Restored");

                foreach (var item in m_StoreController.products.all)
                {
                    if (item.definition.id == IAPItems.remove_ads && item.definition.type == ProductType.NonConsumable
                        && HasPurchaseReceipt(item.receipt))
                    {
                        PlayerPrefs.SetInt(PlayerPrefsHelper.RemoveAds, 1);

                        if (AdsManager.Instance)
                        {
                            AdsManager.Instance.DestroyBannerAd();
                        }

                    }


                    if (item.definition.id == IAPItems.unlock_characters && item.definition.type == ProductType.NonConsumable
                        && HasPurchaseReceipt(item.receipt))
                    {
                        for (int i = 0; i <= 7; i++)
                        {
                            string playerString = $"Player_{i}";
                            PlayerPrefs.SetInt(playerString, 1);
                        }
                    }
                }

                loadingPanel.SetActive(false);
                messagePanel.SetActive(true);
                messageText.text = "Restore Successful";
            }
            else
            {
                Debug.Log("Restore Failed");

                loadingPanel.SetActive(false);
                messagePanel.SetActive(true);
                messageText.text = "Restore Failed";
            }
        }

        private bool HasPurchaseReceipt(string id)
        {
            return !string.IsNullOrEmpty(id);
        }

        public void OnCloseMessagePanelButtonPressed()
        {
            if (SFXManager.Instance)
                SFXManager.Instance.PlayClickSfx();

            loadingPanel.SetActive(false);
            messagePanel.SetActive(false);
        }

        private void AddCoins(int coinsAmount)
        {
            var currentCoins = PlayerPrefs.GetInt(PlayerPrefsHelper.Gold);
            currentCoins += coinsAmount;
            MainMenuManager.Instance.shopPanel.GetComponent<ShopPanel>().coinsDisplayText.text = currentCoins.ToString();
            MainMenuManager.Instance.coinsDisplayText.text = currentCoins.ToString();
            PlayerPrefs.SetInt(PlayerPrefsHelper.Gold, currentCoins);
        }

        private void AddGems(int gemsAmount)
        {
            var curretGems = PlayerPrefs.GetInt(PlayerPrefsHelper.Gems);
            curretGems += gemsAmount;
            MainMenuManager.Instance.shopPanel.GetComponent<ShopPanel>().gemsDisplayText.text = curretGems.ToString();
            MainMenuManager.Instance.bulletsDisplayText.text = curretGems.ToString();
            PlayerPrefs.SetInt(PlayerPrefsHelper.Gems, curretGems);
        }
    }

    public class IAPItems
    {
        public static string remove_ads = "com.raptureplay.remove.ads";
        public static string coins_500 = "com.raptureplay.coins.500";
        public static string coins_800 = "com.raptureplay.coins.800";
        public static string coins_1000 = "com.raptureplay.coins.1000";
        public static string coins_1500 = "com.raptureplay.coins.1500";
        public static string gems_200 = "com.raptureplay.gems.200";
        public static string gems_300 = "com.raptureplay.gems.300";
        public static string gems_500 = "com.raptureplay.gems.500";
        public static string gems_1000 = "com.raptureplay.gems.1000";
        public static string master_bundle = "com.raptureplay.gems.master.bundle";
        public static string mega_bundle = "com.raptureplay.gems.mega.bundle";
        public static string legendary_bundle = "com.raptureplay.gems.legendary.bundle";
        public static string unlock_characters = "com.raptureplay.unlock.all.characters";

    }
}