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

CREATE TABLE coefficients(
    "Id" SERIAL PRIMARY KEY,
    "DateTime" DATE,
    "Coefficient" DECIMAL
--     "FirstCcyId" INTEGER,
--     "SecondCcyId" INTEGER,
--     FOREIGN KEY ("FirstCcyId") REFERENCES currencies("Id"),
--     FOREIGN KEY ("SecondCcyId") REFERENCES currencies("Id")
);

SELECT * FROM "Balances";
SELECT * FROM "Operations";
SELECT * FROM "Coefficients";
SELECT * FROM "Currencies";
SELECT * FROM "BrokerTrades";
SELECT * FROM "ClientTrades";
DROP TABLE "Currencies";
DROP TABLE "Coefficients";
DROP TABLE "BrokerTrades";
DROP TABLE "ClientTrades";
DROP TABLE "Operations";
DROP TABLE "Balances";
DELETE FROM "Balances" where "Id" > 10;
INSERT INTO broker_trade(BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (10, 600, 2, 1);
INSERT INTO client_trade(BrokerCurrencyTradeId, BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (2, 10, 600, 2, 1);

INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (1, 'Russian rouble', 'RUB');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (2, 'Dollar USA', 'USD');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (3, 'Pound sterling', 'GBP');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (4, 'Euro', 'EUR');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (5, 'Japanese Yen', 'JPY');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (6, 'Australian dollar', 'AUD');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (7, 'Dollar canadien', 'CAD');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (8, 'Swiss franc', 'CHF');
INSERT INTO "Currencies" ("Id", "Name", "IsoCode") VALUES (9, 'Yuan', 'CNH');
INSERT INTO "Coefficients" ("Coefficient", "DateTime") VALUES (1.2, now());
INSERT INTO "Coefficients" ("Coefficient", "DateTime", "FirstCcyIsoCode", "SecondCcyIsoCode") VALUES (2.5, now(), 'USD', 'RUB');


INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('RUB', 10000000, 'd8a2e553-fec8-4289-9d61-1cb82dbdb04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('USD', 10000000, 'd8a2e553-fec8-4289-9d61-1cb82d2db04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('GBP', 10000000, 'd8a27553-fec8-4289-9d61-1cb82d1db04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('EUR', 10000000, 'd8a2e553-fec8-4289-9d61-1cb82dbdb04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('JPY', 10000000, 'd1a25553-fec8-4289-9d61-1cb31dbdb04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('AUD', 10000000, 'd8a2e553-fec8-4279-9d61-1cb83dbdb104');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('CAD', 10000000, 'd8a21553-fec8-4289-9d61-1cb82dbdb04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('CHF', 10000000, 'd812e553-f6c8-4289-9d61-1cb52dbdb04b');
INSERT INTO "Balances" ("CurrencyIso", "Amount", "Version") VALUES ('CNH', 10000000, 'd8a2e553-fec8-4689-9d61-1cb821bdb04b');


INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('RUB', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('USD', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('GBP', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('EUR', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('JPY', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('AUD', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('CAD', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('CHF', 10000000, now());
INSERT INTO "Operations" ("CurrencyIso", "Amount", "DateTime") VALUES ('CNH', 10000000, now());