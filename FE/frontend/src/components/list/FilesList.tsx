import { FileZipOutlined, PlusCircleOutlined } from '@ant-design/icons';
import { AddCircle } from '@styled-icons/ionicons-outline';
import { List } from 'antd';
import { usePageContext } from 'context/PageContext';
import { useSiderContext } from 'context/SiderContext';
import React, { Dispatch, SetStateAction } from 'react';
import JwtDecode from 'jwt-decode';
import {
  addFile,
  addUserToSharedSpace,
} from 'service/shared-spaces/shared-spaces';
import styled from 'styled-components';
import { StorageKey } from 'util/enums/storage-keys';
import { useParams } from 'react-router-dom';

const FilesList: React.FC<{
  data?: unknown[] | undefined;
  cardData?: any;
}> = ({ data, cardData }) => {
  const { setIsModalVisible, userTrigger, setUserTrigger } = usePageContext();
  const { id } = useParams();

  console.log(id, 'add');

  return (
    <List
      dataSource={data}
      renderItem={(item: any) => (
        <StyledListItem
          aria-hidden="true"
          onClick={async () => {
            await addFile({
              fileId: item.fileId,
              sharedSpaceID: id,
            });
            setIsModalVisible(false);
            setUserTrigger(!userTrigger);
          }}
          key={item.fileName}
        >
          <List.Item.Meta
            title={item.fileName}
            description={`${item.userName} ${item.userLastName}`}
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

export default FilesList;
