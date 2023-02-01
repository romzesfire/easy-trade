CREATE TABLE broker_trade(
    Id SERIAL PRIMARY KEY,
    BuyAmount DECIMAL,
    SellAmount DECIMAL,
    BuyCcyId INTEGER,
    SellCcyId INTEGER,
    FOREIGN KEY (BuyCcyId) REFERENCES currency(Id),
    FOREIGN KEY (SellCcyId) REFERENCES currency(Id)
);

CREATE TABLE client_trade(
    Id SERIAL PRIMARY KEY,
    BrokerCurrencyTradeId INTEGER,
    BuyAmount DECIMAL,
    SellAmount DECIMAL,
    BuyCcyId INTEGER,
    SellCcyId INTEGER,
    FOREIGN KEY (BrokerCurrencyTradeId) REFERENCES broker_trade(Id) ON DELETE CASCADE,
    FOREIGN KEY (BuyCcyId) REFERENCES currency(Id),
    FOREIGN KEY (SellCcyId) REFERENCES currency(Id)
);

CREATE TABLE currency(
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(20),
    IsoCode CHAR(3) UNIQUE
);

SELECT * FROM "_currencies";
SELECT * FROM "_brokerTrades";

SELECT * FROM "_clientTrades";

DROP TABLE currency;
DROP TABLE broker_trade;
DROP TABLE client_trade;

INSERT INTO broker_trade(BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (10, 600, 2, 1);
INSERT INTO client_trade(BrokerCurrencyTradeId, BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (2, 10, 600, 2, 1);

INSERT INTO currency (Name, IsoCode) VALUES ('Russian rouble', 'RUB');
INSERT INTO currency (Name, IsoCode) VALUES ('Dollar USA', 'USD');
INSERT INTO currency (Name, IsoCode) VALUES ('Euro', 'EUR');
