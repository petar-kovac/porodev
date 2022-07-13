import axios from 'axios';
import { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import { verifyEmail } from 'service/files/files';
import styled from 'styled-components';
import success from '../../assets/success.png';

const EmailVerify = () => {
  const [loading, setLoading] = useState(true);
  const [validUrl, setValidUrl] = useState(false);
  const { email, token } = useParams();

  useEffect(() => {
    verifyEmail(email, token)
      .then((response: any) => {
        setValidUrl(true);
      })
      .catch((error: any) => {
        console.log(error);
        setValidUrl(false);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  if (loading) {
    return (
      <>
        <p>Loading...</p>
      </>
    );
  }

  return (
    <>
      {validUrl ? (
        <div
          style={{
            width: '100vw',
            height: '100vh',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            flexDirection: 'column',
          }}
        >
          <img src={success} alt="success_img" />
          <h1>Email verified successfully</h1>
          <Link to="/login">
            <button type="button">Proceed to login</button>
          </Link>
        </div>
      ) : (
        <h1>404 Not Found</h1>
      )}
    </>
  );
};

export default EmailVerify;
