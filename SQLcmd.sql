CREATE TABLE Registration (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(100),
    Email VARCHAR(100) UNIQUE,
    Phone VARCHAR(15),
    Password VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION sp_insert_user(_fullname VARCHAR, _email VARCHAR, _phone VARCHAR, _password VARCHAR)
RETURNS VOID AS $$
BEGIN
    INSERT INTO Registration(FullName, Email, Phone, Password)
    VALUES (_fullname, _email, _phone, _password);
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION sp_get_all_users()
RETURNS TABLE (Id INT, FullName VARCHAR, Email VARCHAR, Phone VARCHAR, CreatedAt TIMESTAMP)
AS $$
BEGIN
    RETURN QUERY SELECT Id, FullName, Email, Phone, CreatedAt FROM Registration;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION sp_get_user_by_id(_id INT)
RETURNS TABLE (Id INT, FullName VARCHAR, Email VARCHAR, Phone VARCHAR, CreatedAt TIMESTAMP)
AS $$
BEGIN
    RETURN QUERY SELECT Id, FullName, Email, Phone, CreatedAt FROM Registration WHERE Id = _id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION sp_update_user(_id INT, _fullname VARCHAR, _email VARCHAR, _phone VARCHAR)
RETURNS VOID AS $$
BEGIN
    UPDATE Registration SET FullName = _fullname, Email = _email, Phone = _phone WHERE Id = _id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION sp_delete_user(_id INT)
RETURNS VOID AS $$
BEGIN
    DELETE FROM Registration WHERE Id = _id;
END;
$$ LANGUAGE plpgsql;
