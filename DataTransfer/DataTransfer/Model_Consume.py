#coding=utf-8
class Model_Consume(object):
    """description of class"""

    def getCreateTableSql(self,tablename):
        sql = '''create table if not exists '''+tablename+'''(
                         Id INT PRIMARY KEY AUTO_INCREMENT,
                         uid INT,
                         ServerID INT,
                         Date VARCHAR(256),
                         Level INT,
                         Diamond INT,
                         VipLevel INT,
                         ConsumptionWay VARCHAR(256),
                         Param1 VARCHAR(256),
                         Param2 VARCHAR(256),
                         ConsumeItems TEXT,
                         KEY idx_uid (uid)
                         )'''
        return sql
        
    def getInsertSql(self,tablename):
        sql = '''insert into '''+tablename+''' values(NULL,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)'''
        return sql


    def FormatData(self,tablename,arr):
        sql = '''insert into '''+tablename+''' values(NULL,%s,%s,%s,%s,%s,%s,%s,%s,%s,%s)'''
        return sql