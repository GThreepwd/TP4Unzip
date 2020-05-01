/**   __      _______  __          __  _      _____            _             _
     /\ \    / / ____| \ \        / / | |    / ____|          | |           | |
    /  \ \  / / (___    \ \  /\  / /__| |__ | |     ___  _ __ | |_ _ __ ___ | |
   / /\ \ \/ / \___ \    \ \/  \/ / _ \ '_ \| |    / _ \| '_ \| __| '__/ _ \| |
  / ____ \  /  ____) |    \  /\  /  __/ |_) | |___| (_) | | | | |_| | | (_) | |
 /_/    \_\/  |_____/      \/  \/ \___|_.__/ \_____\___/|_| |_|\__|_|  \___/|_|

@Description: Global Configuration
*/

// Config
// ==============================================================================================
var _websocketIp = "{0}"; // IP Address
var _websocketPort = "{1}"; // IP Port
// ==============================================================================================

// - null
// - center
// - stretch
// - scaleToFit
// - scaleToFitCentereds
var _contentSizeType = "scaleToFitCentered"; // null = don't do anything!

var _idlePageTimeout = 60000; // 60s (1000 = 1s) --> used only if set!
var _blinkTimerTime = 800; // 0.8s (1000 = 1s) or  --> project.settings.feedbackBlinkRate

// Video Slot's definition
/*
var _videoSlot[1] = "hls://172.16.126.147:1935/live/mpegts.stream/playlist.m3u8";
var _videoSlot[2] = "hls://qthttp.apple.com.edgesuite.net/1010qwoeiuryfg/sl.m3u8";
var _videoSlot[3] = "";
var _videoSlot[4] = "";
*/

