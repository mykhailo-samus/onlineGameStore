Instruction:
1) to enable event logging: 
  1.1) open VS with administrator rights
  1.2) uncomment line: WriteTo.EventLog("GameStore") in "Configure" method of 
       class "LoggingConfiguration"
2) logs are located in folder "..\OnlineGameStore.Web\Logs"