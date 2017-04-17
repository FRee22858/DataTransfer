#coding=utf-8
class Model_Obtain(object):
    """description of class"""
    #Id =0
    #uid =0
    #ServerID =0
    #Date =0
    #Level =0
    #Diamond =0
    #VipLevel =0
    #ConsumptionWay="",
    #Param1 =0
    #Param2 =0
    #ConsumeItems=""
    def getCreateTableSql(self,tablename):
        sql = '''create table if not exists '''+tablename+'''(
                         Id INT PRIMARY KEY AUTO_INCREMENT,
                         uid INT,
                         ServerID INT,
                         Date VARCHAR(256),
                         Level INT,
                         Diamond INT,
                         VipLevel INT,
                         ObtainWay VARCHAR(256),
                         Param1 INT,
                         Param2 INT,
                         ObtainItems TEXT,
                         KEY idx_uid (uid)
                         )'''
        return sql
        
    def getInsertSql(self,tablename):
        sql = '''insert into '''+tablename+''' values(NULL,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)'''
        return sql


    def FormatData(self,tablename,arr):
        sql = '''insert into '''+tablename+''' values(NULL,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)'''
        return sql


