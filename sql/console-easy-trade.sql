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
SELECT * FROM "brokerTrades";
SELECT * FROM "Coefficients";
SELECT * FROM "Currencies";
DROP TABLE "Currencies";
DROP TABLE "Coefficients";
DROP TABLE "BrokerTrades";
DROP TABLE "ClientTrades";
DROP TABLE "Balances";
DELETE FROM "Coefficients" WHERE "Id"= cast(1 as bigint);
INSERT INTO broker_trade(BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (10, 600, 2, 1);
INSERT INTO client_trade(BrokerCurrencyTradeId, BuyAmount, SellAmount, BuyCcyId, SellCcyId) VALUES (2, 10, 600, 2, 1);

INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Russian rouble', 'RUB');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Dollar USA', 'USD');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Pound sterling', 'GBP');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Euro', 'EUR');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Japanese Yen', 'JPY');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Australian dollar', 'AUD');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Dollar canadien', 'CAD');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Swiss franc', 'CHF');
INSERT INTO "Currencies" ("Name", "IsoCode") VALUES ('Yuan', 'CNH');
INSERT INTO "Coefficients" ("Coefficient", "DateTime") VALUES (1.2, now());


INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (1, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (2, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (3, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (4, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (5, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (6, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (7, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (8, 10000000, now());
INSERT INTO "Balances" ("CurrencyId", "Amount", "DateTime") VALUES (9, 10000000, now());