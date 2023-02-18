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

CREATE TABLE coefficients(
    "Id" SERIAL PRIMARY KEY,
    "DateTime" DATE,
    "Coefficient" DECIMAL
--     "FirstCcyId" INTEGER,
--     "SecondCcyId" INTEGER,
--     FOREIGN KEY ("FirstCcyId") REFERENCES currencies("Id"),
--     FOREIGN KEY ("SecondCcyId") REFERENCES currencies("Id")
);


SELECT * FROM currencies;
SELECT * FROM "brokerTrades";

SELECT * FROM coefficients;

DROP TABLE coefficients;
DROP TABLE "brokerTrades";
DROP TABLE "clientTrades";
DROP TABLE balances;

INSERT INTO broker_trade(BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (10, 600, 2, 1);
INSERT INTO client_trade(BrokerCurrencyTradeId, BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (2, 10, 600, 2, 1);

INSERT INTO currencies ("Name", "IsoCode") VALUES ('Russian rouble', 'RUB');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Dollar USA', 'USD');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Pound sterling', 'GBP');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Euro', 'EUR');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Japanese Yen', 'JPY');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Australian dollar', 'AUD');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Dollar canadien', 'CAD');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Swiss franc', 'CHF');
INSERT INTO currencies ("Name", "IsoCode") VALUES ('Yuan', 'CNH');
INSERT INTO coefficients ("CurrencyId", "Coefficient") VALUES (0, 1.2);


INSERT INTO balances ("CurrencyId", "Amount") VALUES (1, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (2, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (3, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (4, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (5, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (6, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (7, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (8, 10000000);
INSERT INTO balances ("CurrencyId", "Amount") VALUES (9, 10000000);