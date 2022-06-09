import { SettingFilled } from '@ant-design/icons';
import { Dropdown, Menu } from 'antd';
import { Link } from 'react-router-dom';
import styled from 'styled-components';
import { useAuthStateValue } from '../../context/AuthContext';

const PDropdown: React.FC = () => {
  const { logout, isAdmin } = useAuthStateValue();

  const menu = (
    <Menu>
      {!isAdmin && ( // fix this when backend has user impleneted
        <Menu.Item key={1} onClick={() => {}}>
          <Link to="/profile">Profile</Link>
        </Menu.Item>
      )}
      <Menu.Item key={2} onClick={() => logout()}>
        Logout
      </Menu.Item>
    </Menu>
  );
  return (
    <StyledDropdown overlay={menu} placement="bottomLeft" arrow>
      <SettingFilled style={{ fontSize: 24 }} />
    </StyledDropdown>
  );
};

const StyledDropdown = styled(Dropdown)`
  display: flex;
  justify-content: center;
  align-items: center;
  height: 32px;
  width: 32px;
  cursor: pointer;
`;

export default PDropdown;
