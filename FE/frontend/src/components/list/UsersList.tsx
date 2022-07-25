import { FileZipOutlined, PlusCircleOutlined } from '@ant-design/icons';
import { AddCircle } from '@styled-icons/ionicons-outline';
import { List } from 'antd';
import { usePageContext } from 'context/PageContext';
import { useSiderContext } from 'context/SiderContext';
import React, { Dispatch, SetStateAction } from 'react';
import JwtDecode from 'jwt-decode';
import { addUserToSharedSpace } from 'service/shared-spaces/shared-spaces';
import styled from 'styled-components';
import { StorageKey } from 'util/enums/storage-keys';

const UsersList: React.FC<{
  data?: unknown[] | undefined;
  cardData: any;
  setImageParameters?: Dispatch<SetStateAction<string[]>>;
}> = ({ data, setImageParameters = () => undefined, cardData }) => {
  const { setIsModalVisible } = usePageContext();

  return (
    <List
      dataSource={data}
      renderItem={(item: any) => (
        <StyledListItem
          aria-hidden="true"
          onClick={async () => {
            console.log('ok');
            await addUserToSharedSpace({
              userToAddId: cardData.ownerId,
              sharedSpaceID: cardData.id,
            });
            setIsModalVisible(false);
          }}
          key={item.fileName}
        >
          <List.Item.Meta
            title={item.email}
            description={`${item.name} ${item.lastname}`}
          />
          <StyledSelect>{/* <PlusCircleOutlined /> */}</StyledSelect>
        </StyledListItem>
      )}
    />
  );
};

const StyledSelect = styled.div`
  cursor: pointer;
`;
const StyledListItem = styled(List.Item)`
  cursor: pointer;
  /* &:hover,
  &:active,
  &:focus {
    border: 2px solid red;
  } */
`;

const StyledDescription = styled.div`
  display: flex;
  justify-content: space-between;
`;

export default UsersList;
