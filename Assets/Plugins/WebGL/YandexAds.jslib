mergeInto(LibraryManager.library, {

    ShowInterstitialAd: function () {
        if (typeof ysdk !== 'undefined') {
            ysdk.adv.showFullscreenAdv();
        }
    },

    ShowRewardedAd: function (callbackPtr) {
        if (typeof ysdk !== 'undefined') {
            ysdk.adv.showRewardedVideo({
                callbacks: {
                    onClose: function () {

                    },
                    onRewarded: function () {
                        unityInstance.SendMessage('YandexBridge', 'OnRewarded');
                    }
                }
            });
        }
    }
});