#!/usr/bin/env python3
import os
import gzip
import io
import xml.etree.ElementTree as ET
import json 
import sys

import xml.dom.NodeFilter;

#import SegmentInfo
from SegmentInfo import SegmentInfo

'''
The global vars
'''
projectPath = os.getcwd()

proceededSegments = []
mSegmentDictionary = []
files = {}

jsonPr = {}
jsonPr["pages"] = {}
jsonPr["subpages"] = {}
jsonPr["palettes"] = []
jsonPr["fonts"] = []

def main(args = sys.argv[1:]):
    
    for i in range(len(sys.argv)):
        if i == 0:
            print ("Script: %s" % sys.argv[0])
        else:
            print ("%d. Argument: %s" % (i,sys.argv[i]))

    print("Test")
    
'''
{
    settings
    pages: {
            name: {...settings..., buttons: [...settings...]},
            name: {...settings..., buttons: [...settings...]},
            name: {...settings..., buttons: [...settings...]}
            }
    subpages: {
            name: {...settings..., buttons: [...settings...]},
            name: {...settings..., buttons: [...settings...]},
            name: {...settings..., buttons: [...settings...]}
            }
}

https://jsoneditoronline.org/

'''
def createJSON(file):
    tree = ET.parse(file)
    
    #
    for palettes in tree.findall("paletteData"):
        for palette in palettes:
            color = {}
            #print(palette.tag)
            #Sprint(palette.text)
            for p in palette:
                color[p.tag] = p.text
            jsonPr["palettes"].append(color)

    #
    for fonts in tree.findall("fontList"):
        for fnt in fonts:
            font = {}
            for f in fnt:
                font[f.tag] = f.text
            jsonPr["fonts"].append(font)

    #
    for pages in tree.findall("page[@type='page']"): 
        page = {}
        page["buttons"] = []
        for p in pages:
            #
            if p.tag == "name":
                name = p.text
            elif p.tag == "button":
                button = {}
                for e in p:
                    button[e.tag] = e.text
                page["buttons"].append(button)
            else:
                page[p.tag] = p.text
        jsonPr["pages"][name] = page
    #
    for subpages in tree.findall("page[@type='subpage']"): 
        subpage = {}
        for p in subpages:
            #
            if p.tag == "name":
                name = p.text
            elif p.tag == "button":
                button = {}
                subpage["buttons"] = []
                for e in p:
                    button[e.tag] = e.text
                subpage["buttons"].insert(len(subpage["buttons"]), button)
            else:
                subpage[p.tag] = p.text
        jsonPr["subpages"][name] = subpage

'''

'''
def writeJSONfile():
    f = open(projectPath + '/' + projectName + "/_project.json", 'w')
    f.write(json.dumps(jsonPr))
    f.close()
    #
    f = open(projectPath + '/' + projectName + "/_project.js", 'w')
    f.write("var _project = " + json.dumps(jsonPr))
    f.close()

'''

'''
def readTp4file(fileName="Simple1.TP4"):
    global projectName
    #
    file = open(fileName, "rb")
    #
    print("Opening file: ", file.name)
    #
    projectName = os.path.splitext(file.name)[0]

    #skip the first 12 bytes
    file.seek(12)
    #
    segmentInfo = file.read(14)
    #
    while len(segmentInfo) == 14:
        #
        segmentData = file.read(512)
        #
        segment = SegmentInfo(segmentInfo, segmentData)
        
        #
        segmentInfo = file.read(14)
        #
        
        mSegmentDictionary.insert(segment.number, segment)
    #
    readSegments()

'''

'''
def readSegments():
    fileName = ""
    for item in mSegmentDictionary:

        if item.number not in proceededSegments:
            #

            proceededSegments.append(item.number)
            #
            if item.length == 0:
                fileName = item.data.decode("utf-8")
                print("Read file: ", fileName)
                files[fileName] = {"name": os.path.splitext(fileName)[0], "ext": os.path.splitext(fileName)[1], "data": bytes()}
            else:
                #just one part of the segment
                if item.prevNumber == 0 and item.nextNumber == 0:
                    #
                    if fileName: 
                         files[fileName]["data"] = item.data


                #
                elif item.prevNumber == 0 and item.nextNumber > 0:
                    #
                    groupSegment = item
                    #
                    while groupSegment.length > 0 or groupSegment.nextNumber > 0:
                        if fileName:
                            files[fileName]["data"] += groupSegment.data
                        #
                        if groupSegment.nextNumber == 0:
                            break
                        #
                        proceededSegments.append(groupSegment.nextNumber)
                        #
                        groupSegment = mSegmentDictionary[groupSegment.nextNumber]
    #
    createFileStructure()
                        
'''

'''
def createFileStructure():
    #
    if not os.path.exists(projectPath + '/' + projectName):
        os.makedirs(projectPath + '/' + projectName)
    else:
        #delete everything
        for root, dirs, files_infolder in os.walk(projectPath + '/' + projectName, topdown=False):
            for name in files_infolder:
                os.remove(os.path.join(root, name))
            for name in dirs:
                os.rmdir(os.path.join(root, name))
    #
    os.makedirs(projectPath + '/' + projectName + "/xml")
    os.makedirs(projectPath + '/' + projectName + "/images")
    os.makedirs(projectPath + '/' + projectName + "/fonts")
    os.makedirs(projectPath + '/' + projectName + "/audio")
    #os.makedirs(projectPath + '/' + projectName + "/tmp")
    #
    for file in files:
        #print(files[file])
        #
        ext = ""
        #
        if files[file]["ext"] == ".xma":
            #check if <?xml version="1.0"?>
            if b'\x3c\x3f\x78\x6d\x6c\x20\x76\x65\x72\x73\x69\x6f\x6e\x3d\x22\x31\x2e\x30\x22\x3f\x3e' in files[file]["data"]:
                f = open(projectPath + '/' + projectName + "/xml/" + files[file]["name"] + ".xml", 'wb')
                f.write(bytes(files[file]["data"]))
                f.close()
            else:
                #extract it with gzip
                try:
                    gz = gzip.open(io.BytesIO(files[file]["data"]), 'r')
                    #
                    f = open(projectPath + '/' + projectName + "/xml/" + files[file]["name"] + ".xml", 'wb')
                    f.write(gz.read())
                    f.close()
                except Exception as e:
                    print(e)
            #
            createJSON(projectPath + '/' + projectName + "/xml/" + files[file]["name"] + ".xml")
        elif files[file]["ext"] == ".xml":
            #extract it with gzip
            #
            try:
                gz = gzip.open(io.BytesIO(files[file]["data"]), 'r')
                #
                f = open(projectPath + '/' + projectName + "/xml/" + files[file]["name"] + ".xml", 'wb')
                f.write(gz.read())
                f.close()
            except Exception as e:
                print(e)
            #
            createJSON(projectPath + '/' + projectName + "/xml/" + files[file]["name"] + ".xml")
        elif files[file]["ext"] == ".png":
            #save only the content don't do anything.
            #
            f = open(projectPath + '/' + projectName + "/images/" + files[file]["name"] + files[file]["ext"], "wb")
            f.write(bytes(files[file]["data"]))
            f.close()
        elif files[file]["ext"] == ".jpg":
            #save only the content don't do anything.
            #
            f = open(projectPath + '/' + projectName + "/images/" + files[file]["name"] + files[file]["ext"], "wb")
            f.write(bytes(files[file]["data"]))
            f.close()
        elif files[file]["ext"] == ".ttf":
            #save only the content don't do anything.
            #
            f = open(projectPath + '/' + projectName + "/fonts/" + files[file]["name"] + files[file]["ext"], "wb")
            f.write(bytes(files[file]["data"]))
            f.close()
        elif files[file]["ext"] == ".ttc":
            #save only the content don't do anything.
            #
            f = open(projectPath + '/' + projectName + "/fonts/" + files[file]["name"] + files[file]["ext"], "wb")
            f.write(bytes(files[file]["data"]))
            f.close()
        elif files[file]["ext"] == ".wav":
            #save only the content don't do anything.
            #
            f = open(projectPath + '/' + projectName + "/audio/" + files[file]["name"] + files[file]["ext"], "wb")
            f.write(bytes(files[file]["data"]))
            f.close()
        elif files[file]["ext"] == ".mp3":
            #save only the content don't do anything.
            #
            f = open(projectPath + '/' + projectName + "/audio/" + files[file]["name"] + files[file]["ext"], "wb")
            f.write(bytes(files[file]["data"]))
            f.close()
    writeJSONfile()

main()

readTp4file()
