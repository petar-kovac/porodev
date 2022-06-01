import { Layout } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';
import PDropdown from '../../components/dropdown/Dropdown';
import { useAuthStateValue } from '../../context/AuthContext';

const { Header } = Layout;

const PHeader: FC = () => {
  const { loggedUser } = useAuthStateValue();

  return (
    <StyledHeader style={{ color: 'white' }}>
      <StyledLogo>Header</StyledLogo>
      <StyledButtons>
        <StyledUserName> {loggedUser?.name}</StyledUserName>
        <StyledUserName> {loggedUser?.lastname} </StyledUserName>
        <PDropdown />
      </StyledButtons>
    </StyledHeader>
  );
};

const StyledUserName = styled.div`
  font-size: 20px;
  font-weight: 500;
`;
const StyledButtons = styled.div`
  display: flex;
  align-items: center;
  gap: 10px;
`;
const StyledHeader = styled(Header)`
  background-color: blue;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;
const StyledLogo = styled.div`
  color: #fff;
  font-size: 20px;
  font-weight: 500;
`;

export default PHeader;
