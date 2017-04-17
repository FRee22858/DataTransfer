#coding=utf-8
import DBManager
import Log

class DataAnalysis_Obtain():
     m_tableName = ''
     arr=[]
     db = None
     def __init__(self,tablename,arr):
         self.m_tableName=tablename 
         self.arr = arr
         self.db = DBManager.DBManager()

     def analysis(self):
        items =[]
        con=["",""]
        tempcon=[]
        for i in range(1, len(self.arr)):
            if (i<=9):
                items.append(self.arr[i])
            elif(i>9):
                if i%2==0:
                    con[0]=self.arr[i]
                else:
                    con[1]=self.arr[i]
                    val =con[0]+':'+con[1]
                    tempcon.append(val)
        strValue=''  
        for i in range(0, len(tempcon)):  
              if i>0:
                 strValue=strValue+"|"
              strValue = strValue+tempcon[i];
                          
        items.append(strValue)

        if self.db == None:
           Log.error("dbManager is None!")
           return -1
        else:
            if self.db.dbSwitch == 1:
                 try:
                     bRet=self.db.insert(self.m_tableName,items)
                     return bRet
                 except Exception as e:
                     Log.error("DataAnalysis_Obtain.analysis() db error code: %s"%(str(e)))
                     return -1
            else:
                return 0          
               



