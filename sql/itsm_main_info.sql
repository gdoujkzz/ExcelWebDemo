CREATE TABLE `ITSM_MAIN_INFO`(
  `Id`  VARCHAR(32),
  `Title` VARCHAR(255),
  `Bill_NO` VARCHAR(255),
  `SecondCategory` VARCHAR(255),
  `Creator` VARCHAR(255),
  `BusinessDept` VARCHAR(255),
  `CurrentSolveUser` VARCHAR(255),
  `SolveUser` VARCHAR(255),
  `CreateTime` DATETIME,
  `Response` VARCHAR(255),
  `Satisfaction` VARCHAR(255),
  `Solve` VARCHAR(255) ,
  `STATUS` VARCHAR(255),
  PRIMARY KEY(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='ITSM主表';
