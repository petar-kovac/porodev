import { FC } from 'react';
import { Link } from 'react-router-dom';

const EmailVerified: FC = () => {
  return (
    <>
      <h1>You are already verified.</h1>
      <Link to="/login">
        <button type="button">Proceed to login</button>
      </Link>
    </>
  );
};

export default EmailVerified;
