import { UserOutlined } from '@ant-design/icons';
import { Button, Modal, Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import useGroupData from 'pages/user/groups/hooks/useGroupData';
import { Dispatch, FC, SetStateAction, ReactNode } from 'react';
import { postPassword, postProfile } from 'service/files/files';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import {
  createSharedSpace,
  getAllSharedSpaces,
} from 'service/shared-spaces/shared-spaces';
import styled from 'styled-components';
import { IFilesCard } from 'types/card-data';

interface IPModalProps {
  title?: string;
  content?: ReactNode;
  onOk?: any;
  data?: any;
  onCancel?: any;
  setModalData?: any;
  modalData?: any | null;
  passData?: any;
  setPassData?: any;
  inputField?: string;
}

const PModal: FC<IPModalProps> = ({
  title,
  content,
  modalData,
  data,
  setModalData = () => undefined,
  onOk,
  onCancel,
  inputField,
  passData,
  setPassData,
}) => {
  const { isModalVisible, setIsModalVisible, setSharedSpaceId, sharedSpaceId } =
    usePageContext();
  const { setData } = useGroupData();

  const handleOk = async () => {
    if (inputField === 'sharedspace') {
      const space = await createSharedSpace(modalData);
      const res = await getAllSharedSpaces();
      setSharedSpaceId(!sharedSpaceId);
      setIsModalVisible(false);
    } else if (inputField === 'password') {
      await postPassword(passData);
      setIsModalVisible(false);
    } else {
      await postProfile(modalData);
      setIsModalVisible(false);
    }
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  return (
    <>
      <StyledFilesModal
        title={title}
        visible={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}

        // footer={[
        //   <div className="footer-content">
        //     <StyledFilesButton onClick={handleCancel}>Cancel</StyledFilesButton>
        //     <StyledFilesButton type="primary" icon={<DownloadOutlined />}>
        //       Download
        //     </StyledFilesButton>
        //   </div>,
        // ]}
      >
        <>
          {inputField === 'firstname' && (
            <Input
              onChange={(e) =>
                setModalData({
                  ...modalData,
                  name: e.target.value,
                })
              }
              value={modalData?.content as string}
            />
          )}
          {inputField === 'lastname' && (
            <Input
              onChange={(e) =>
                setModalData({ ...modalData, lastname: e.target.value })
              }
              value={modalData?.content as string}
            />
          )}

          {inputField === 'password' && (
            <>
              <p>Old password:</p>
              <Input
                onChange={(e) =>
                  setPassData({ ...passData, oldPassword: e.target.value })
                }
                value={passData?.content as string}
              />
              <p>New password:</p>
              <Input
                onChange={(e) =>
                  setPassData({ ...passData, newPassword: e.target.value })
                }
                value={passData?.content as string}
              />
            </>
          )}

          {inputField === 'sharedspace' && (
            <Input
              onChange={(e) =>
                setModalData({ ...modalData, name: e.target.value })
              }
              value={modalData?.content as string}
            />
          )}
        </>
      </StyledFilesModal>
    </>
  );
};

const StyledFilesModal = styled(Modal).attrs({
  'data-testid': 'modal',
})`
  .ant-modal-header {
    border-radius: 1.2rem;
    background-color: rgba(220, 220, 220, 0.1);
    border: 1px solid #fff;
    border-bottom: 1px solid #eee;
  }

  .ant-modal-title {
    color: #555;
    letter-spacing: 0.5px;
    font-size: 1.8rem;
  }

  .ant-modal-content {
    border-radius: 1.2rem;
    box-shadow: 1px 3px 4px rgba(255, 255, 255, 0.4);
  }

  .ant-modal-footer {
    padding: 1.4rem 2.2rem;
    border-radius: 1.6rem;

    button {
      border-radius: 0.8rem;
    }

    .footer-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;

export default PModal;
