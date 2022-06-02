import { FC } from 'react';
import styled from 'styled-components';
import { useAuthStateValue } from '../../context/AuthContext';

const Profile: FC = () => {
  const { isAuthenticated, testMessage, loggedUser } = useAuthStateValue();

  return (
    <StyledPage>
      <h1>Logged user:</h1>
      <h3>Name - {loggedUser?.name}</h3>
      <h3>Last name - {loggedUser?.lastname}</h3>
      <h3>Email - {loggedUser?.email}</h3>
      <h3>Password - {loggedUser?.password}</h3>
      <h3>Department - {loggedUser?.department}</h3>
      <h3>Position - {loggedUser?.position}</h3>
      <h3>Avatar - {loggedUser?.avatarUrl}</h3>
    </StyledPage>
  );
};

const StyledPage = styled.div`
  padding: 20px;
`;
export default Profile;
