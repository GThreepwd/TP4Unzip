class SegmentInfo:

    def __init__(self, segmentInfo, segmentData):
        self.info = segmentInfo
        self.number = int.from_bytes(segmentInfo[:4], byteorder='little')
        self.prevNumber = int.from_bytes(segmentInfo[4:8], byteorder='little')
        self.nextNumber = int.from_bytes(segmentInfo[8:12], byteorder='little')
        self.length = int.from_bytes(segmentInfo[12:14], byteorder='little')
        #
        #print("Segment Nr.: ", self.segmentNumber)
        #print("Previous Segment Nr.: ", self.segmentPrevNumber)
        #print("Next Segment Nr.: ", self.segmentNextNumber)
        #print("Segment length: ", self.length)
        #
        if self.length > 0:
            self.data = segmentData[:self.length]
        else:
            end = segmentData.find(b'\x00')
            #
            self.data = segmentData[:end]
        #
        #if segmentData.find(b'\x1f\x8b') != -1:
        #    self.type = "gz"
        #elif segmentData.find(b'\x89\x50\x4e\x47\x0d\x0a\x1a\x0a') != -1:
        #    self.type = "png"
        #elif segmentData.find(b'\x52\x49\x46\x46') != -1:
        #    self.type = "wav"
        #elif segmentData.find(b'\x00\x01\x00\x00') != -1:
        #    self.type = "ttf"
        #else:
        #    self.type = None



    def area(self):
        return self.x * self.y