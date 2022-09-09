import { Layout } from 'antd';
import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';

import JwtDecode from 'jwt-decode';

import PDropdown from 'components/dropdown/PDropdown';
import logoImage from 'assets/newLogo.png';
import { StorageKey } from 'util/enums/storage-keys';
import { findUserById } from 'service/authorization/authorization';
import { usePageContext } from 'context/PageContext';
import { Navigate } from '@styled-icons/ionicons-outline';
import { useNavigate } from 'react-router-dom';

const { Header } = Layout;

const PHeader: FC = () => {
  const [userData, setUserData] = useState<any>(null);

  const { isModalVisible, setIsModalVisible } = usePageContext();

  const accessToken = localStorage.getItem(StorageKey.ACCESS_TOKEN);

  useEffect(() => {
    if (accessToken) {
      const { Id }: any = JwtDecode(accessToken);
      findUserById(Id)
        .then((res) => {
          const { data } = res;
          setUserData(data);
          console.log(userData);
        })
        .catch((error) => console.log(error));
    }
  }, [isModalVisible]);

  return (
    <StyledHeader style={{ color: 'white' }}>
      <StyledLogo src={logoImage} />
      <StyledButtons>
        <StyledUserName>{userData?.name}</StyledUserName>
        <StyledUserName>{userData?.lastname}</StyledUserName>
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
const StyledHeader = styled(Header).attrs({
  'data-testid': 'header',
})`
  background-color: #1990ff;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-right: 20px;
  padding-left: 10px;
  border-bottom-left-radius: 10px;
  border-bottom-right-radius: 10px;
  box-shadow: 0px 3px 6px rgba(34, 25, 25, 0.3);
  z-index: 1000;
`;
const StyledLogo = styled.img`
  width: 90px;
`;

export default PHeader;
