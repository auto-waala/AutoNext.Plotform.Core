-- =====================================================
-- Script 5: Seed Brands Data
-- =====================================================

INSERT INTO public.brands (name, code, slug, description, logo_url, country_of_origin, founded_year, applicable_categories, display_order, is_active) VALUES

    -- Japanese Brands
    ('Toyota', 'TOYOTA', 'toyota', 'Japanese automotive manufacturer', '/icons/brands/toyota.svg', 'Japan', 1937, '["CARS", "SUV", "TRUCKS", "VANS", "HYBRID"]', 1, true),
    ('Honda', 'HONDA', 'honda', 'Japanese manufacturer of cars, motorcycles, and power equipment', '/icons/brands/honda.svg', 'Japan', 1948, '["CARS", "BIKES", "SUV", "TRUCKS", "SCOOTERS"]', 2, true),
    ('Nissan', 'NISSAN', 'nissan', 'Japanese automobile manufacturer', '/icons/brands/nissan.svg', 'Japan', 1933, '["CARS", "SUV", "TRUCKS", "EV"]', 3, true),
    ('Suzuki', 'SUZUKI', 'suzuki', 'Japanese automotive and motorcycle manufacturer', '/icons/brands/suzuki.svg', 'Japan', 1909, '["CARS", "BIKES", "SUV", "SCOOTERS"]', 4, true),
    ('Mazda', 'MAZDA', 'mazda', 'Japanese automotive manufacturer', '/icons/brands/mazda.svg', 'Japan', 1920, '["CARS", "SUV"]', 5, true),
    ('Subaru', 'SUBARU', 'subaru', 'Japanese automotive manufacturer known for AWD', '/icons/brands/subaru.svg', 'Japan', 1953, '["CARS", "SUV"]', 6, true),
    ('Mitsubishi', 'MITSUBISHI', 'mitsubishi', 'Japanese automotive manufacturer', '/icons/brands/mitsubishi.svg', 'Japan', 1970, '["CARS", "SUV", "TRUCKS"]', 7, true),
    ('Lexus', 'LEXUS', 'lexus', 'Japanese luxury vehicle division of Toyota', '/icons/brands/lexus.svg', 'Japan', 1989, '["CARS", "SUV", "LUXURY", "HYBRID"]', 8, true),
    ('Isuzu', 'ISUZU', 'isuzu', 'Japanese commercial vehicle and diesel engine manufacturer', '/icons/brands/isuzu.svg', 'Japan', 1916, '["TRUCKS", "SUV"]', 9, true),
    ('Daihatsu', 'DAIHATSU', 'daihatsu', 'Japanese compact and microcar manufacturer', '/icons/brands/daihatsu.svg', 'Japan', 1907, '["CARS", "TRUCKS"]', 10, true),

    -- Korean Brands
    ('Hyundai', 'HYUNDAI', 'hyundai', 'South Korean automotive manufacturer', '/icons/brands/hyundai.svg', 'South Korea', 1967, '["CARS", "SUV", "EV"]', 11, true),
    ('Kia', 'KIA', 'kia', 'South Korean automotive manufacturer', '/icons/brands/kia.svg', 'South Korea', 1944, '["CARS", "SUV", "EV"]', 12, true),
    ('Genesis', 'GENESIS', 'genesis', 'South Korean luxury vehicle division of Hyundai', '/icons/brands/genesis.svg', 'South Korea', 2015, '["CARS", "SUV", "LUXURY"]', 13, true),
    ('SsangYong', 'SSANGYONG', 'ssangyong', 'South Korean SUV and off-road vehicle manufacturer', '/icons/brands/ssangyong.svg', 'South Korea', 1954, '["SUV", "TRUCKS"]', 14, true),

    -- German Brands
    ('Volkswagen', 'VW', 'volkswagen', 'German automotive manufacturer', '/icons/brands/vw.svg', 'Germany', 1937, '["CARS", "SUV", "VANS"]', 15, true),
    ('BMW', 'BMW', 'bmw', 'German luxury automotive manufacturer', '/icons/brands/bmw.svg', 'Germany', 1916, '["CARS", "BIKES", "SUV", "LUXURY"]', 16, true),
    ('Mercedes-Benz', 'MERCEDES', 'mercedes-benz', 'German luxury automotive brand', '/icons/brands/mercedes.svg', 'Germany', 1926, '["CARS", "SUV", "TRUCKS", "VANS", "LUXURY"]', 17, true),
    ('Audi', 'AUDI', 'audi', 'German luxury automotive manufacturer', '/icons/brands/audi.svg', 'Germany', 1909, '["CARS", "SUV", "LUXURY"]', 18, true),
    ('Porsche', 'PORSCHE', 'porsche', 'German sports car manufacturer', '/icons/brands/porsche.svg', 'Germany', 1931, '["CARS", "SUV", "SPORTS"]', 19, true),
    ('Opel', 'OPEL', 'opel', 'German automotive manufacturer', '/icons/brands/opel.svg', 'Germany', 1862, '["CARS", "SUV", "VANS"]', 20, true),

    -- American Brands
    ('Ford', 'FORD', 'ford', 'American automotive manufacturer', '/icons/brands/ford.svg', 'USA', 1903, '["CARS", "SUV", "TRUCKS", "VANS"]', 21, true),
    ('Chevrolet', 'CHEVY', 'chevrolet', 'American automotive brand', '/icons/brands/chevrolet.svg', 'USA', 1911, '["CARS", "SUV", "TRUCKS", "VANS"]', 22, true),
    ('Tesla', 'TESLA', 'tesla', 'American electric vehicle manufacturer', '/icons/brands/tesla.svg', 'USA', 2003, '["CARS", "SUV", "EV"]', 23, true),
    ('Jeep', 'JEEP', 'jeep', 'American off-road vehicle brand', '/icons/brands/jeep.svg', 'USA', 1941, '["SUV", "TRUCKS"]', 24, true),
    ('Dodge', 'DODGE', 'dodge', 'American automotive brand', '/icons/brands/dodge.svg', 'USA', 1900, '["CARS", "SUV", "TRUCKS", "SPORTS"]', 25, true),
    ('GMC', 'GMC', 'gmc', 'American commercial vehicle brand', '/icons/brands/gmc.svg', 'USA', 1911, '["SUV", "TRUCKS", "VANS"]', 26, true),
    ('Cadillac', 'CADILLAC', 'cadillac', 'American luxury vehicle brand', '/icons/brands/cadillac.svg', 'USA', 1902, '["CARS", "SUV", "LUXURY"]', 27, true),
    ('Lincoln', 'LINCOLN', 'lincoln', 'American luxury vehicle brand by Ford', '/icons/brands/lincoln.svg', 'USA', 1917, '["CARS", "SUV", "LUXURY"]', 28, true),
    ('RAM', 'RAM', 'ram', 'American truck and commercial vehicle brand', '/icons/brands/ram.svg', 'USA', 2010, '["TRUCKS", "VANS"]', 29, true),
    ('Rivian', 'RIVIAN', 'rivian', 'American electric vehicle and adventure brand', '/icons/brands/rivian.svg', 'USA', 2009, '["TRUCKS", "SUV", "EV"]', 30, true),

    -- European Brands
    ('Volvo', 'VOLVO', 'volvo', 'Swedish automotive manufacturer', '/icons/brands/volvo.svg', 'Sweden', 1927, '["CARS", "SUV", "TRUCKS"]', 31, true),
    ('Jaguar', 'JAGUAR', 'jaguar', 'British luxury vehicle brand', '/icons/brands/jaguar.svg', 'UK', 1922, '["CARS", "SUV", "LUXURY"]', 32, true),
    ('Land Rover', 'LANDROVER', 'land-rover', 'British off-road luxury vehicles', '/icons/brands/landrover.svg', 'UK', 1948, '["SUV", "LUXURY"]', 33, true),
    ('Peugeot', 'PEUGEOT', 'peugeot', 'French automotive manufacturer', '/icons/brands/peugeot.svg', 'France', 1882, '["CARS", "SUV", "VANS"]', 34, true),
    ('Renault', 'RENAULT', 'renault', 'French automotive manufacturer', '/icons/brands/renault.svg', 'France', 1899, '["CARS", "SUV", "VANS", "EV"]', 35, true),
    ('Citroen', 'CITROEN', 'citroen', 'French automotive manufacturer', '/icons/brands/citroen.svg', 'France', 1919, '["CARS", "SUV", "VANS"]', 36, true),
    ('Fiat', 'FIAT', 'fiat', 'Italian automotive manufacturer', '/icons/brands/fiat.svg', 'Italy', 1899, '["CARS", "VANS"]', 37, true),
    ('Alfa Romeo', 'ALFAROMEO', 'alfa-romeo', 'Italian luxury and sports car manufacturer', '/icons/brands/alfaromeo.svg', 'Italy', 1910, '["CARS", "SUV", "SPORTS", "LUXURY"]', 38, true),
    ('Ferrari', 'FERRARI', 'ferrari', 'Italian luxury sports car manufacturer', '/icons/brands/ferrari.svg', 'Italy', 1939, '["CARS", "SPORTS", "LUXURY"]', 39, true),
    ('Lamborghini', 'LAMBORGHINI', 'lamborghini', 'Italian luxury supercar manufacturer', '/icons/brands/lamborghini.svg', 'Italy', 1963, '["CARS", "SUV", "SPORTS", "LUXURY"]', 40, true),
    ('Maserati', 'MASERATI', 'maserati', 'Italian luxury vehicle manufacturer', '/icons/brands/maserati.svg', 'Italy', 1914, '["CARS", "SUV", "LUXURY", "SPORTS"]', 41, true),
    ('SEAT', 'SEAT', 'seat', 'Spanish automotive manufacturer', '/icons/brands/seat.svg', 'Spain', 1950, '["CARS", "SUV"]', 42, true),
    ('Skoda', 'SKODA', 'skoda', 'Czech automotive manufacturer under VW Group', '/icons/brands/skoda.svg', 'Czech Republic', 1895, '["CARS", "SUV", "VANS"]', 43, true),

    -- Indian Brands
    ('Tata Motors', 'TATA', 'tata-motors', 'Indian multinational automotive manufacturer', '/icons/brands/tata.svg', 'India', 1945, '["CARS", "SUV", "TRUCKS", "VANS", "EV"]', 44, true),
    ('Mahindra', 'MAHINDRA', 'mahindra', 'Indian automotive manufacturer known for SUVs and off-road vehicles', '/icons/brands/mahindra.svg', 'India', 1945, '["CARS", "SUV", "TRUCKS", "EV"]', 45, true),
    ('Maruti Suzuki', 'MARUTI', 'maruti-suzuki', 'Indian subsidiary of Suzuki, largest car manufacturer in India', '/icons/brands/maruti.svg', 'India', 1981, '["CARS", "SUV", "VANS"]', 46, true),
    ('Bajaj Auto', 'BAJAJ', 'bajaj-auto', 'Indian motorcycle and three-wheeler manufacturer', '/icons/brands/bajaj.svg', 'India', 1945, '["BIKES", "SCOOTERS"]', 47, true),
    ('TVS Motor', 'TVS', 'tvs-motor', 'Indian motorcycle and scooter manufacturer', '/icons/brands/tvs.svg', 'India', 1978, '["BIKES", "SCOOTERS"]', 48, true),
    ('Hero MotoCorp', 'HERO', 'hero-motocorp', 'Indian motorcycle and scooter manufacturer', '/icons/brands/hero.svg', 'India', 1984, '["BIKES", "SCOOTERS"]', 49, true),
    ('Royal Enfield', 'ROYALENFIELD', 'royal-enfield', 'Indian motorcycle manufacturer known for classic bikes', '/icons/brands/royalenfield.svg', 'India', 1901, '["BIKES"]', 50, true),
    ('Ola Electric', 'OLA', 'ola-electric', 'Indian electric scooter manufacturer', '/icons/brands/ola.svg', 'India', 2017, '["SCOOTERS", "EV"]', 51, true),
    ('Ather Energy', 'ATHER', 'ather-energy', 'Indian electric scooter manufacturer', '/icons/brands/ather.svg', 'India', 2013, '["SCOOTERS", "EV"]', 52, true),
    ('Force Motors', 'FORCE', 'force-motors', 'Indian commercial and utility vehicle manufacturer', '/icons/brands/force.svg', 'India', 1958, '["TRUCKS", "VANS", "SUV"]', 53, true),

    -- Chinese Brands
    ('BYD', 'BYD', 'byd', 'Chinese electric vehicle and battery manufacturer', '/icons/brands/byd.svg', 'China', 1995, '["CARS", "SUV", "TRUCKS", "EV"]', 54, true),
    ('MG Motor', 'MG', 'mg-motor', 'British-origin brand now owned by SAIC of China', '/icons/brands/mg.svg', 'China', 1924, '["CARS", "SUV", "EV"]', 55, true),
    ('Haval', 'HAVAL', 'haval', 'Chinese SUV brand under Great Wall Motors', '/icons/brands/haval.svg', 'China', 2013, '["SUV"]', 56, true),
    ('NIO', 'NIO', 'nio', 'Chinese electric vehicle manufacturer', '/icons/brands/nio.svg', 'China', 2014, '["CARS", "SUV", "EV"]', 57, true),

    -- Motorcycle Brands
    ('Yamaha', 'YAMAHA', 'yamaha', 'Japanese motorcycle and scooter manufacturer', '/icons/brands/yamaha.svg', 'Japan', 1955, '["BIKES", "SCOOTERS"]', 58, true),
    ('Kawasaki', 'KAWASAKI', 'kawasaki', 'Japanese motorcycle manufacturer', '/icons/brands/kawasaki.svg', 'Japan', 1896, '["BIKES"]', 59, true),
    ('Harley-Davidson', 'HARLEY', 'harley-davidson', 'American motorcycle manufacturer', '/icons/brands/harley.svg', 'USA', 1903, '["BIKES"]', 60, true),
    ('Ducati', 'DUCATI', 'ducati', 'Italian motorcycle manufacturer', '/icons/brands/ducati.svg', 'Italy', 1926, '["BIKES"]', 61, true),
    ('KTM', 'KTM', 'ktm', 'Austrian motorcycle manufacturer', '/icons/brands/ktm.svg', 'Austria', 1934, '["BIKES"]', 62, true),
    ('Triumph', 'TRIUMPH', 'triumph', 'British motorcycle manufacturer', '/icons/brands/triumph.svg', 'UK', 1902, '["BIKES"]', 63, true),
    ('Aprilia', 'APRILIA', 'aprilia', 'Italian motorcycle manufacturer', '/icons/brands/aprilia.svg', 'Italy', 1945, '["BIKES", "SCOOTERS"]', 64, true),
    ('Benelli', 'BENELLI', 'benelli', 'Italian motorcycle manufacturer', '/icons/brands/benelli.svg', 'Italy', 1911, '["BIKES"]', 65, true),
    ('CFMoto', 'CFMOTO', 'cfmoto', 'Chinese motorcycle and ATV manufacturer', '/icons/brands/cfmoto.svg', 'China', 1989, '["BIKES"]', 66, true),

    -- Bicycle Brands
    ('Trek', 'TREK', 'trek', 'American bicycle manufacturer', '/icons/brands/trek.svg', 'USA', 1976, '["CYCLES"]', 67, true),
    ('Giant', 'GIANT', 'giant', 'Taiwanese bicycle manufacturer', '/icons/brands/giant.svg', 'Taiwan', 1972, '["CYCLES"]', 68, true),
    ('Specialized', 'SPECIALIZED', 'specialized', 'American bicycle brand', '/icons/brands/specialized.svg', 'USA', 1974, '["CYCLES"]', 69, true),
    ('Cannondale', 'CANNONDALE', 'cannondale', 'American bicycle manufacturer', '/icons/brands/cannondale.svg', 'USA', 1971, '["CYCLES"]', 70, true),
    ('Scott', 'SCOTT', 'scott', 'Swiss bicycle and sports equipment brand', '/icons/brands/scott.svg', 'Switzerland', 1958, '["CYCLES"]', 71, true),
    ('Hero Cycles', 'HEROCYCLES', 'hero-cycles', 'Indian bicycle manufacturer', '/icons/brands/herocycles.svg', 'India', 1956, '["CYCLES"]', 72, true),
    ('Firefox Bikes', 'FIREFOX', 'firefox-bikes', 'Indian bicycle brand', '/icons/brands/firefox.svg', 'India', 2005, '["CYCLES"]', 73, true);

-- Verify
SELECT COUNT(*) AS total_brands_inserted FROM public.brands;
SELECT name, code, country_of_origin FROM public.brands ORDER BY display_order;