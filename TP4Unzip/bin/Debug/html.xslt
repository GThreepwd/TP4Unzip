<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:output method="html" encoding="utf-8" indent="yes" />
  
  <xsl:template match="/">
    
    <xsl:text disable-output-escaping='yes'>&lt;!doctype html&gt;&#xD;&#xA;</xsl:text>
    
    <html lang="de-DE">
      <head>
        <meta charset="UTF-8" />
        <title>
          <xsl:value-of select="root/page/name"/>
        </title>
        
        <style>

        <!-- Page Properties -->
        <!--
        <root>
          <page type="page">
            <sr number="1">
              <bs></bs>           ==> Border Style
              <cb></cb>           ==> Border Color
              <cf>Grey5</cf>      ==> Fill Color
              <ct>Black</ct>      ==> Text Color
              <ec>#000000FF</ec>  ==> Text Effect color
              <fi>20</fi>         ==> Font Number (Ref -> $fnt.xml)
            </sr>
        -->
              
        body {
          background-color:<xsl:apply-templates select="/root/page/sr[1]/cf" />;
          width:<xsl:value-of select="root/page/width"/>px;
          height:<xsl:value-of select="root/page/height"/>px;
          
          /* -ms-transform: scale(1.5, 1.5); /* IE 9 */
          /* -webkit-transform: scale(1.5, 1.5); /* Safari */
          /* transform: scale(1.5, 1.5); /* Standard syntax */
          
          transform-origin: 0 0;
          overflow:hidden;
          padding:0px;
          margin:0px;
          border-style:solid;border-width:1px;
        }
        </style>
        
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      </head>
      
      <body>

        <!-- Buttons -->
        <xsl:apply-templates select="//button[@type='general']" />
        
        <script>
          // Resizes current window (150%)
          // window.resizeTo(<xsl:value-of select="root/page/width * 1.5"/>,  <xsl:value-of select="root/page/height * 1.5"/>);
          // window.resizeTo(1024 * 1.53, 768 * 1.805);
          window.focus(); // Sets focus to the new window
        </script>
        
      </body>
    </html>
  </xsl:template>

  <xsl:template match="button[@type='general']">
    
      <button>
        
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
          <fi>36</fi>              ==> Font Number (Ref -> $fnt.xml)
          <te>Button A</te>        ==> Text
          <jt>1</jt>               ==> Text Justification (Top-Left)
          <ww>1</ww>               ==> Word Wrap
        </sr>
        -->
        
        <!-- Id -->
        <xsl:attribute name="id">
          <xsl:value-of select="na"/>
        </xsl:attribute>
        
        <!-- Style -->
        <xsl:attribute name="style">
          width:<xsl:value-of select="wt"/>px;
          height:<xsl:value-of select="ht"/>px;
          background-color:<xsl:apply-templates select="sr[1]/cf" />;
          position: absolute;
          left:<xsl:value-of select="lt"/>px;
          top:<xsl:value-of select="tp"/>px;
          border-style:solid;border-width:<xsl:apply-templates select="sr[1]/bs" />px;
          border-color:<xsl:apply-templates select="sr[1]/cb" />;
          color:<xsl:apply-templates select="sr[1]/ct" />;
          padding:0px;
        </xsl:attribute>
        
        <!-- Bitmap -->
        <xsl:if test="sr[1]/bm!=''"><img src="../Images/{sr[1]/bm}" alt="{sr[1]/bm}" style="width:100%;height:auto;" /></xsl:if>
        
        <!-- Text -->
        <xsl:value-of select="sr[1]/te"/>
        
      </button>

  </xsl:template>

  <!-- Border Style-->
  <xsl:template match="bs|sr/bs">
    <xsl:choose>
      <xsl:when test=".=''">0</xsl:when>
      <xsl:when test=".='Single Line'">1</xsl:when>
      <xsl:when test=".='Double Line'">2</xsl:when>
      <xsl:when test=".='Quad Line'">4</xsl:when>
      <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- Color Names TP4 -->
  <xsl:template match="sr/cf|cb|ct">
    <xsl:choose>
      
      <xsl:when test=".='VeryLight'">#FF0000</xsl:when>
      <xsl:when test=".='Light'"    >#DF0000</xsl:when>
      <xsl:when test=".=''"         >#BF0000</xsl:when>
      <xsl:when test=".='Medium'"   >#9F0000</xsl:when>
      <xsl:when test=".='Dark'"     >#7F0000</xsl:when>
      <xsl:when test=".='VeryDark'" >#5F0000</xsl:when>

      <xsl:when test=".='VeryLightRed'">#FF0000</xsl:when>
      <xsl:when test=".='LightRed'"    >#DF0000</xsl:when>
      <xsl:when test=".='Red'"         >#BF0000</xsl:when>
      <xsl:when test=".='MediumRed'"   >#9F0000</xsl:when>
      <xsl:when test=".='DarkRed'"     >#7F0000</xsl:when>
      <xsl:when test=".='VeryDarkRed'" >#5F0000</xsl:when>

      <xsl:when test=".='VeryLightOrange'">#FF8000</xsl:when>
      <xsl:when test=".='LightOrange'"    >#DF7000</xsl:when>
      <xsl:when test=".='Orange'"         >#BF6000</xsl:when>
      <xsl:when test=".='MediumOrange'"   >#9F5000</xsl:when>
      <xsl:when test=".='DarkOrange'"     >#7F4000</xsl:when>
      <xsl:when test=".='VeryDarkOrange'" >#5F3000</xsl:when>

      <xsl:when test=".='VeryLightYellow'">#FFFF00</xsl:when>
      <xsl:when test=".='LightYellow'"    >#DFDF00</xsl:when>
      <xsl:when test=".='Yellow'"         >#BFBF00</xsl:when>
      <xsl:when test=".='MediumYellow'"   >#9F9F00</xsl:when>
      <xsl:when test=".='DarkYellow'"     >#7F7F00</xsl:when>
      <xsl:when test=".='VeryDarkYellow'" >#5F5F00</xsl:when>
      
      <xsl:when test=".='VeryLightLime'">#80FF00</xsl:when>
      <xsl:when test=".='LightLime'"    >#70DF00</xsl:when>
      <xsl:when test=".='Lime'"         >#60BF00</xsl:when>
      <xsl:when test=".='MediumLime'"   >#509F00</xsl:when>
      <xsl:when test=".='DarkLime'"     >#407F00</xsl:when>
      <xsl:when test=".='VeryDarkLime'" >#305F00</xsl:when>

      <xsl:when test=".='VeryLightGreen'">#00FF00</xsl:when>
      <xsl:when test=".='LightGreen'"    >#00DF00</xsl:when>
      <xsl:when test=".='Green'"         >#00BF00</xsl:when>
      <xsl:when test=".='MediumGreen'"   >#009F00</xsl:when>
      <xsl:when test=".='DarkGreen'"     >#007F00</xsl:when>
      <xsl:when test=".='VeryDarkGreen'" >#005F00</xsl:when>
      
      <xsl:when test=".='VeryLightMint'">#00FF80</xsl:when>
      <xsl:when test=".='LightMint'"    >#00DF70</xsl:when>
      <xsl:when test=".='Mint'"         >#00BF60</xsl:when>
      <xsl:when test=".='MediumMint'"   >#009F50</xsl:when>
      <xsl:when test=".='DarkMint'"     >#007F40</xsl:when>
      <xsl:when test=".='VeryDarkMint'" >#005F30</xsl:when>

      <xsl:when test=".='VeryLightCyan'">#00FFFF</xsl:when>
      <xsl:when test=".='LightCyan'"    >#00DFDF</xsl:when>
      <xsl:when test=".='Cyan'"         >#00BFBF</xsl:when>
      <xsl:when test=".='MediumCyan'"   >#009F9F</xsl:when>
      <xsl:when test=".='DarkCyan'"     >#007F7F</xsl:when>
      <xsl:when test=".='VeryDarkCyan'" >#005F5F</xsl:when>

      <xsl:when test=".='VeryLightAqua'">#0080FF</xsl:when>
      <xsl:when test=".='LightAqua'"    >#0070DF</xsl:when>
      <xsl:when test=".='Aqua'"         >#0060BF</xsl:when>
      <xsl:when test=".='MediumAqua'"   >#00509F</xsl:when>
      <xsl:when test=".='DarkAqua'"     >#00407F</xsl:when>
      <xsl:when test=".='VeryDarkAqua'" >#00305F</xsl:when>

      <xsl:when test=".='VeryLightBlue'">#0000FF</xsl:when>
      <xsl:when test=".='LightBlue'"    >#0000DF</xsl:when>
      <xsl:when test=".='Blue'"         >#0000BF</xsl:when>
      <xsl:when test=".='MediumBlue'"   >#00009F</xsl:when>
      <xsl:when test=".='DarkBlue'"     >#00007F</xsl:when>
      <xsl:when test=".='VeryDarkBlue'" >#00005F</xsl:when>

      <xsl:when test=".='VeryLightPurple'">#8000FF</xsl:when>
      <xsl:when test=".='LightPurple'"    >#7000DF</xsl:when>
      <xsl:when test=".='Purple'"         >#6000BF</xsl:when>
      <xsl:when test=".='MediumPurple'"   >#50009F</xsl:when>
      <xsl:when test=".='DarkPurple'"     >#40007F</xsl:when>
      <xsl:when test=".='VeryDarkPurple'" >#30005F</xsl:when>

      <xsl:when test=".='VeryLightMagenta'">#FF00FF</xsl:when>
      <xsl:when test=".='LightMagenta'"    >#DF00DF</xsl:when>
      <xsl:when test=".='Magenta'"         >#BF00BF</xsl:when>
      <xsl:when test=".='MediumMagenta'"   >#9F009F</xsl:when>
      <xsl:when test=".='DarkMagenta'"     >#7F007F</xsl:when>
      <xsl:when test=".='VeryDarkMagenta'" >#5F005F</xsl:when>

      <xsl:when test=".='VeryLightPink'">#FF0080</xsl:when>
      <xsl:when test=".='LightPink'"    >#DF0070</xsl:when>
      <xsl:when test=".='Pink'"         >#BF0060</xsl:when>
      <xsl:when test=".='MediumPink'"   >#9F0050</xsl:when>
      <xsl:when test=".='DarkPink'"     >#7F0040</xsl:when>
      <xsl:when test=".='VeryDarkPink'" >#5F0030</xsl:when>

      <xsl:when test=".='White'" >#FFFFFF</xsl:when>
      <xsl:when test=".='Grey1'" >#EEEEEE</xsl:when>
      <xsl:when test=".='Grey2'" >#DDDDDD</xsl:when>
      <xsl:when test=".='Grey3'" >#CCCCCC</xsl:when>
      <xsl:when test=".='Grey4'" >#BBBBBB</xsl:when>
      <xsl:when test=".='Grey5'" >#AAAAAA</xsl:when>
      <xsl:when test=".='Grey6'" >#999999</xsl:when>
      <xsl:when test=".='Grey7'" >#888888</xsl:when>
      <xsl:when test=".='Grey8'" >#777777</xsl:when>
      <xsl:when test=".='Grey9'" >#666666</xsl:when>
      <xsl:when test=".='Grey10'">#555555</xsl:when>
      <xsl:when test=".='Grey11'">#444444</xsl:when>
      <xsl:when test=".='Grey12'">#333333</xsl:when>
      <xsl:when test=".='Grey13'">#222222</xsl:when>
      <xsl:when test=".='Grey14'">#111111</xsl:when>
      <xsl:when test=".='Black'" >#000000</xsl:when>
      
      <xsl:when test=".='Transparent'" >transparent</xsl:when>
      
      <xsl:otherwise><xsl:value-of select="."/></xsl:otherwise>

    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
