// Information:
// ==============================================================================================
// Aufgrund der Kompatibilität für den Internet-Explorer,
// erfolgen alle Zugriffe auf die Dataset-Erweiterungen mittels 'getAttribute' und 'setAttribute'
// ==============================================================================================

var NavigationId = "navigation";
var ContentId = "content";

function InitializeProject()
{
  ProcessJson();
}

function LoadJson()
{
  var lRequest = new XMLHttpRequest();

  if (lRequest.overrideMimeType)
    lRequest.overrideMimeType("application/json");

  lRequest.open('GET', './proj.json', true);

  lRequest.onreadystatechange = function ()
  {
    if (lRequest.readyState == 4 && lRequest.status == "200")
    {
      Proj = JSON.parse(lRequest.responseText);

      ProcessJson();
    }
  };

  lRequest.send(null);
}

function ProcessJson()
{
  document.title = Proj.project.projectInfo.jobName;

  // Create navigation
  var lPages = Proj.project.pageList;

  console.log("Processing Navigation: PageCount=" + lPages.length);

  for (i in lPages)
  {
    var lEntries = lPages[i].pageEntry

    for (e in lEntries)
    {
      var lName = ReplaceAll(lEntries[e].name, "_", " ");

      var a = document.createElement('a');

      a.href = "#top";
      a.innerHTML = lName
      a.title = lName;

      switch (lPages[i].type)
      {
        case "page": a.className = "navPage"; break;
        case "subpage": a.className = "navSubpage"; break;
      }

      a.addEventListener("click", ShowPage);

      a.setAttribute("data-page-Id", lEntries[e].pageID); // IE

      var li = document.createElement("li");

      // li.className = "active";

      li.appendChild(a)

      document.getElementById(NavigationId).appendChild(li);
    }
  }

  lPages = Proj.pages;

  // Create pages
  for (i in lPages)
  {
    var lPage = lPages[i].page;

    console.log("Processing Page: Name=" + lPage.name);

    // Create div
    var lDiv = document.createElement("div");

    lDiv.id = lPage.pageID;

    lDiv.setAttribute("data-type", lPage.type);
    lDiv.setAttribute("data-name", lPage.name);

    if (lPage.group !== undefined)
      lDiv.setAttribute("data-group", lPage.group);

    lDiv.style.position = "absolute";

    if (lPage.left !== undefined)
      lDiv.style.left = lPage.left + "px";

    if (lPage.top !== undefined)
      lDiv.style.top = lPage.top + "px";

    lDiv.style.width = lPage.width + "px";
    lDiv.style.height = lPage.height + "px";
    lDiv.style.borderStyle = "solid";
    lDiv.style.borderWidth = GetBorderWidth(lPage.sr.bs) + "px"; // Border Style
    lDiv.style.borderColor = GetWebColor(lPage.sr.cb); // Border Color
    lDiv.style.backgroundColor = GetWebColor(lPage.sr.cf); // Fill Color
    lDiv.style.color = GetWebColor(lPage.sr.ct); // Text Color

    // Text
    if (lPage.sr.te !== undefined)
    {
      var lSpan = document.createElement("span");

      lSpan.innerHTML = lPage.sr.te;

      // <jt>1</jt> ==> Text Justification
      AssignTextJustification(lSpan, lPage.sr.jt);

      // Absolute
      if (lPage.sr.jt == "0")
      {
        // <tx>20</tx> ==> Text X Offset
        // <ty>20</ty> ==> Text Y Offset
        lSpan.style.left = lButton.sr[0].tx + "px";
        lSpan.style.top = lButton.sr[0].ty + "px";
      }

      lDiv.appendChild(lSpan);
    }

    // Font
    // <fi>36</fi> ==> Font Number(Ref -> $fnt.xml)
    AssignFont(lDiv, lPage.sr.fi);

    lDiv.style.display = "none"; // "hidden"
    lDiv.style.margin = "auto auto";
    lDiv.style.zIndex = -2;

    // Create buttons
    for (b in lPage.button)
    {
      var lButton = lPage.button[b];
      /*

      <!--
      <button type="general">
        <bi>1</bi>                 ==> Index
        <na>Button A</na>          ==> Name
        <li>1</li>                 ==>
        <lt>16</lt>                ==> Position Left
        <tp>16</tp>                ==> Position Top
        <wt>125</wt>               ==> Width
        <ht>125</ht>               ==> Height
        <zo>1</zo>                 ==> Z-Order ([<]Back, [>]:Top)
        <hs>bounding</hs>          ==> Touch Style  (bounding, passThru)
        <bs></bs>                  ==> Border Style (-> TAB General)
        <da>1</da>                 ==> Disabled
        <hd>1</hd>                 ==> Hidden
        <fb>momentary</fb>         ==> Feedback
        <ap>2</ap>                 ==> Address Port
        <ad>22</ad>                ==> Address Code
        <cp>3</cp>                 ==> Channel Port
        <ch>33</ch>                ==> Channel Code
        <vt>rel</vt>               ==> Level Control Type
        <lp>4</lp>                 ==> Level Port
        <lv>44</lv>                ==> Level Code
        <rl>0</rl>                 ==> Range Low
        <rh>255</rh>               ==> Range High
        <ac di="0" />              ==>
        <sr number="1">            ==> State Off
          <bs>Quad Line</bs>       ==> Border Style
          <mi>Image.jpg</mi>       ==> Chameleon Image
          <cb>Grey7</cb>           ==> Border Color
          <cf>VeryLightOrange</cf> ==> Fill Color
          <ct>Black</ct>           ==> Text Color
          <ec>#000000FF</ec>       ==> Text Effect color
          <oo>100</oo>             ==> Overall Opacity
          <bm>Image.png</bm>       ==> Bitmap
          <jb>1</jb>               ==> Bitmap Justification (Top-Left)
          <bx>20</bx>              ==> Bitmap X Offset
          <by>20</by>              ==> Bitmap Y Offset
          <fi>36</fi>              ==> Font Number (Ref -> $fnt.xml)
          <te>Button A</te>        ==> Text
          <jt>1</jt>               ==> Text Justification (Top-Left)
          <tx>20</tx>              ==> Text X Offset
          <ty>20</ty>              ==> Text Y Offset
          <ww>1</ww>               ==> Word Wrap
        </sr>
        <sr number="2">            ==> State On
        ...
        </sr>
      </button>
      */

      // Create container for button (for align text)
      var lBtnContainer = document.createElement("div");

      lBtnContainer.style.display = "table";

      // Create button
      var lBtn = document.createElement("button");

      lBtn.id = lButton.na; // Name

      lBtn.setAttribute("data-type", lButton.type); // General
      lBtn.setAttribute("data-id", lButton.bi); // Button-Id

      lBtn.style.width = lButton.wt + "px";
      lBtn.style.height = lButton.ht + "px";
      lBtn.style.borderStyle = "solid";
      lBtn.style.borderWidth = GetBorderWidth(lButton.sr[0].bs) + "px"; // Border Style
      lBtn.style.borderColor = GetWebColor(lButton.sr[0].cb); // Border Color
      lBtn.style.backgroundColor = GetWebColor(lButton.sr[0].cf); // Fill Color
      lBtn.style.color = GetWebColor(lButton.sr[0].ct); // Text Color
      lBtn.style.position = "absolute";
      lBtn.style.left = lButton.lt + "px";
      lBtn.style.top = lButton.tp + "px";
      lBtn.style.padding = 0;

      // Text
      if (lButton.sr[0].te !== undefined)
      {
        var lSpan = document.createElement("span");

        lSpan.innerHTML = lButton.sr[0].te;

        // <jt>1</jt> ==> Text Justification
        AssignTextJustification(lSpan, lButton.sr[0].jt);

        // Absolute
        if (lButton.sr[0].jt == "0")
        {
        // <tx>20</tx> ==> Text X Offset
        // <ty>20</ty> ==> Text Y Offset
          lSpan.style.left = lButton.sr[0].tx + "px";
          lSpan.style.top = lButton.sr[0].ty + "px";
        }

        lBtn.appendChild(lSpan);
      }

      // Create Image (Bitmap)
      if (lButton.sr[0].bm !== undefined)
      {
        lBtn.style.backgroundImage = "url(images/" + lButton.sr[0].bm + ")";;
        lBtn.style.backgroundRepeat = "no-repeat";
        lBtn.style.backgroundPosition = GetBitmapJustification(lButton.sr[0].jb)

        // Absolute
        if (lButton.sr[0].jb == "0")
        {
          // <bx>20</bx> ==> Bitmap X Offset
          // <by>20</by> ==> Bitmap Y Offset
          lBtn.style.backgroundPositionX = lButton.sr[0].bx + "px";
          lBtn.style.backgroundPositionY = lButton.sr[0].by + "px";
        }
      }

      // Font
      // <fi>36</fi> ==> Font Number(Ref -> $fnt.xml)
      AssignFont(lBtn, lButton.sr[0].fi);

      // lBtnContainer.appendChild(lBtn);

      lDiv.appendChild(lBtn);
    }

    document.getElementById(ContentId).appendChild(lDiv);
  }

  // Show main page
  var lContainer = document.getElementById(ContentId);

  if (Proj.project.panelSetup.powerUpPage != '')
  {
    if (lContainer.children[Proj.project.panelSetup.powerUpPage] !== undefined)
      lContainer.children[Proj.project.panelSetup.powerUpPage].style.display = "block";
    else
      lContainer.children[0].style.display = "block";
  }
  else
    lContainer.children[0].style.display = "block";
}

function ShowPage()
{
  var lPage = document.getElementById(this.getAttribute("data-page-Id"));
  var lPages = document.getElementById(ContentId).children;

  if (lPage.getAttribute("data-type") == "page")
  {
    // Page
    for (var i = 0; i < lPages.length; i++)
      lPages[i].style.display = "none";
  }
  else
  {
    // SubPage

    // Hide popups in same group
    if (lPage.hasAttribute("data-group"))
    {
      var lGroup = lPage.getAttribute("data-group");

      // Hide Popup
      for (var i = 0; i < lPages.length; i++)
      {
        if (lPages[i].id == lPage.id)
          continue;

        if (lPages[i].getAttribute("data-group") == lGroup)
          lPages[i].style.display = "none";
      }
    }
  }

  // Show page/popup
  lPage.style.display = "block";
}

function GetBitmapJustification(justification)
{
  switch (justification)
  {
    /*
    0: absolute
    1: top-left
    2: top-middle
    3: top-right
    4: center-left
    5: center-middle (undefined)
    6: center-right
    7: bottom-left
    8: bottom-middle
    9: bottom-right
    */

    case "0": return "";
    case "1": return "top left";
    case "2": return "top center";
    case "3": return "top right";
    case "4": return "center left";
    case "5": return "center center";
    case "6": return "center right";
    case "7": return "bottom left";
    case "8": return "bottom center";
    case "9": return "bottom right";
  }

  // undefined
  return "center center";
}

function GetBorderWidth(borderStyle)
{
  switch (borderStyle)
  {
    case "Single Line": return 1;
    case "Double Line": return 2;
    case "Quad Line": return 4;
  }

  return 0;
}

function AssignFont(button, fontNumber)
{
  if (fontNumber == undefined)
    return;

  // lProj.fontList.font
  /*
  "number": "32",
  "file": "verdanab.ttf",
  "fileSize": "211316",
  "faceIndex": "0",
  "name": "Verdana",
  "subfamilyName": "Bold",
  "fullName": "Verdana Bold",
  "size": "14",
  "usageCount": "2"
  */
  for (var i in Proj.fontList.font)
  {
    if (Proj.fontList.font[i].number == fontNumber)
    {
      button.style.fontFamily = Proj.fontList.font[i].name;
      button.style.fontSize = Proj.fontList.font[i].size + "pt";
      button.style.fontWeight = Proj.fontList.font[i].subfamilyName;

      return;
    }
  }
}

function AssignTextJustification(button, justification)
{
  if (justification == undefined)
    return;

  switch (justification)
  {
    /*
    0: absolute
    1: top-left
    2: top-middle
    3: top-right
    4: center-left
    5: center-middle (undefined)
    6: center-right
    7: bottom-left
    8: bottom-middle
    9: bottom-right
    */

    case "0":
      {
        // absolute
        button.style.position = "absolute";
      }
    case "1":
      {
        // top-left
        button.style.position = "absolute";
        button.style.left = 0;
        button.style.top = 0;
        return;
      }
    case "2":
      {
        // top-middle
        button.style.position = "absolute";
        button.style.left = 0;
        button.style.right = 0;
        button.style.top = 0;
        return;
      }
    case "3":
      {
        // top-right
        button.style.position = "absolute";
        button.style.right = 0;
        button.style.top = 0;
        return;
      }
    case "4":
      {
        // center-left
        button.style.textAlign = "left";
        button.style.display = "block";
        return;
      }
    case "5":
      {
        // center-middle
        return;
      }
    case "6":
      {
        // center-right
        button.style.textAlign = "right";
        button.style.display = "block";
        return;
      }
    case "7":
      {
        // bottom-left
        button.style.position = "absolute";
        button.style.left = 0;
        button.style.bottom = 0;
        return;
      }
    case "8":
      {
        // bottom-middle
        button.style.position = "absolute";
        button.style.left = 0;
        button.style.right = 0;
        button.style.bottom = 0;
        return;
      }
    case "9":
      {
        // bottom-right
        button.style.position = "absolute";
        button.style.right = 0;
        button.style.bottom = 0;
        return;
      }
  }

  // undefined
  return;
}

function GetWebColor(color)
{
  switch (color)
  {
    case "VeryLightRed": return "#FF0000";
    case "LightRed": return "#DF0000";
    case "Red": return "#BF0000";
    case "MediumRed": return "#9F0000";
    case "DarkRed": return "#7F0000";
    case "VeryDarkRed": return "#5F0000";

    case "VeryLightOrange": return "#FF8000";
    case "LightOrange": return "#DF7000";
    case "Orange": return "#BF6000";
    case "MediumOrange": return "#9F5000";
    case "DarkOrange": return "#7F4000";
    case "VeryDarkOrange": return "#5F3000";

    case "VeryLightYellow": return "#FFFF00";
    case "LightYellow": return "#DFDF00";
    case "Yellow": return "#BFBF00";
    case "MediumYellow": return "#9F9F00";
    case "DarkYellow": return "#7F7F00";
    case "VeryDarkYellow": return "#5F5F00";

    case "VeryLightLime": return "#80FF00";
    case "LightLime": return "#70DF00";
    case "Lime": return "#60BF00";
    case "MediumLime": return "#509F00";
    case "DarkLime": return "#407F00";
    case "VeryDarkLime": return "#305F00";

    case "VeryLightGreen": return "#00FF00";
    case "LightGreen": return "#00DF00";
    case "Green": return "#00BF00";
    case "MediumGreen": return "#009F00";
    case "DarkGreen": return "#007F00";
    case "VeryDarkGreen": return "#005F00";

    case "VeryLightMint": return "#00FF80";
    case "LightMint": return "#00DF70";
    case "Mint": return "#00BF60";
    case "MediumMint": return "#009F50";
    case "DarkMint": return "#007F40";
    case "VeryDarkMint": return "#005F30";

    case "VeryLightCyan": return "#00FFFF";
    case "LightCyan": return "#00DFDF";
    case "Cyan": return "#00BFBF";
    case "MediumCyan": return "#009F9F";
    case "DarkCyan": return "#007F7F";
    case "VeryDarkCyan": return "#005F5F";

    case "VeryLightAqua": return "#0080FF";
    case "LightAqua": return "#0070DF";
    case "Aqua": return "#0060BF";
    case "MediumAqua": return "#00509F";
    case "DarkAqua": return "#00407F";
    case "VeryDarkAqua": return "#00305F";

    case "VeryLightBlue": return "#0000FF";
    case "LightBlue": return "#0000DF";
    case "Blue": return "#0000BF";
    case "MediumBlue": return "#00009F";
    case "DarkBlue": return "#00007F";
    case "VeryDarkBlue": return "#00005F";

    case "VeryLightPurple": return "#8000FF";
    case "LightPurple": return "#7000DF";
    case "Purple": return "#6000BF";
    case "MediumPurple": return "#50009F";
    case "DarkPurple": return "#40007F";
    case "VeryDarkPurple": return "#30005F";

    case "VeryLightMagenta": return "#FF00FF";
    case "LightMagenta": return "#DF00DF";
    case "Magenta": return "#BF00BF";
    case "MediumMagenta": return "#9F009F";
    case "DarkMagenta": return "#7F007F";
    case "VeryDarkMagenta": return "#5F005F";

    case "VeryLightPink": return "#FF0080";
    case "LightPink": return "#DF0070";
    case "Pink": return "#BF0060";
    case "MediumPink": return "#9F0050";
    case "DarkPink": return "#7F0040";
    case "VeryDarkPink": return "#5F0030";

    case "White": return "#FFFFFF";
    case "Grey1": return "#EEEEEE";
    case "Grey2": return "#DDDDDD";
    case "Grey3": return "#CCCCCC";
    case "Grey4": return "#BBBBBB";
    case "Grey5": return "#AAAAAA";
    case "Grey6": return "#999999";
    case "Grey7": return "#888888";
    case "Grey8": return "#777777";
    case "Grey9": return "#666666";
    case "Grey10": return "#555555";
    case "Grey11": return "#444444";
    case "Grey12": return "#333333";
    case "Grey13": return "#222222";
    case "Grey14": return "#111111";
    case "Black": return "#000000";

    case "Transparent": return "transparent";
  }

  // IE -> Remove Alphacolor
  // #AEFFFFFF
  return color.substring(0, 7);
}

function EscapeRegExp(str)
{
  return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
}

function ReplaceAll(str, find, replace)
{
  return str.replace(new RegExp(EscapeRegExp(find), 'g'), replace);
}
