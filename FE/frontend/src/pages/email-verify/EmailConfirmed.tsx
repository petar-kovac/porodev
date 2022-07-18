import { Link } from 'react-router-dom';

const EmailConfirmed = () => {
  return (
    <>
      <h1>EmailConfirmed</h1>
      <Link to="/login">
        <button type="button">Proceed to login</button>
      </Link>
    </>
  );
};

export default EmailConfirmed;